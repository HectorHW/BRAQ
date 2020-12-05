using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Antlr4.Runtime;

namespace BRAQ
{
    public class ILVisitor : BRAQParserBaseVisitor<int>
    {
        private ILGenerator il;
        private Dictionary<IToken, BRAQParser.Var_stmtContext> variable_to_declaration;

        private readonly BRAQParser.Var_stmtContext[] locals;
        
        private Dictionary<ParserRuleContext, Type> type_dict;

        private Dictionary<IToken, MethodInfo> function_table;

        private Dictionary<IToken, TyperVisitor.OwnMethodInfo> user_function_table;

        private TyperVisitor.TyperResult typerResult; 
        
        //for break&continue generation
        private Label? loop_begin = null;
        private Label? loop_end = null;


        private Label? method_end = null;

        public ILVisitor(ILGenerator il,
            TyperVisitor.TyperResult typerResult
            )
        {
            this.il = il;
            variable_to_declaration = typerResult.var_to_def;

            locals = typerResult.local_defs.ToArray();
            type_dict = typerResult.type_dict;
            function_table = typerResult.token_to_outer_function;
            user_function_table = typerResult.token_to_user_function;
            current_method = typerResult.methodInfo;
            this.typerResult = typerResult;
        }

        private TyperVisitor.OwnMethodInfo current_method;
        private LocalBuilder return_holder = null;

        public override int VisitStmt(BRAQParser.StmtContext context)
        {
            if (context.containing_def != null) return 0; //все объявления функций обрабатываются отдельно
            return base.VisitStmt(context);
        }

        public override int VisitIf_stmt(BRAQParser.If_stmtContext context)
        {
            //evaluate condition
            context.cond.Accept(this);
            Label false_branch = il.DefineLabel();
            Label after_blocks = il.DefineLabel();

            il.Emit(OpCodes.Brfalse_S, false_branch);
            
            context.then_branch.Accept(this);

            il.Emit(OpCodes.Br_S, after_blocks);

            il.MarkLabel(false_branch);
            
            if (context.else_branch != null)
            {
                context.else_branch.Accept(this);
            }
            else
            {
                il.Emit(OpCodes.Nop);
            }
            il.MarkLabel(after_blocks);
            
            
            return 0;
        }

        public override int VisitLiteral(BRAQParser.LiteralContext context)
        {
            if (context.num != null)
            {
                il.Emit(OpCodes.Ldc_I4, int.Parse(context.num.Text));
            }else if (context.str != null)
            {
                //строки хронятся вместе с кавычками, уберём их
                string based = context.str.Text;
                string unquoted = based.Substring(1, based.Length - 2);
                
                il.Emit(OpCodes.Ldstr, unquoted);
            }
            
            else if (context.double_num != null)
            {
                Console.WriteLine($"double constant {context.double_num.Text}");
                il.Emit(OpCodes.Ldc_R8, double.Parse(context.double_num.Text, new CultureInfo("en-us")));
            }
            
            else if (context.var_node_!=null)
            {
                context.var_node_.Accept(this); //variable
            }
            else if (context.containing_group != null)
            {
                context.containing_group.Accept(this);
            }
            else
            {
                throw new NotImplementedException();
            }

            return 0;
        }

        public override int VisitWhile_loop_stmt(BRAQParser.While_loop_stmtContext context)
        {
            
            //сгенерируем условие
            Label beforeCond = il.DefineLabel();
            
            var prev_loop_begin = loop_begin;
            var prev_loop_end = loop_end;
            
            il.MarkLabel(beforeCond); //точка перед условием
            
            Label after_loop_body = il.DefineLabel();
            
            loop_begin = beforeCond;
            loop_end = after_loop_body;
            
            if (context.cond != null){ // возможно сгенерируем условие
                context.cond.Accept(this); 
                il.Emit(OpCodes.Brfalse_S, after_loop_body);
            }


            context.body.Accept(this);
            il.Emit(OpCodes.Br_S ,beforeCond); // прыгаем к проверке условий
            il.MarkLabel(after_loop_body); //точка выхода

            loop_begin = prev_loop_begin;
            loop_end = prev_loop_end;
            
            return 0;
        }

        public override int VisitVar_node(BRAQParser.Var_nodeContext context)
        {
            
            if(variable_to_declaration.ContainsKey(context.id_name)){
                BRAQParser.Var_stmtContext declaration_point = variable_to_declaration[context.id_name];
                int var_id = Array.IndexOf(locals, declaration_point);
                il.Emit(OpCodes.Ldloc, var_id);
                return 0;
            }

            int n = current_method.arguments.IndexOf(
                current_method.arguments.First(x => x.a==context.id_name.Text)
                );
                
            il.Emit(OpCodes.Ldarg, n);

            return 0;
        }

        public override int VisitBreak_stmt(BRAQParser.Break_stmtContext context)
        {
            Debug.Assert(loop_end != null); //always true bc of checks
            il.Emit(OpCodes.Br_S, loop_end.Value); 
            return 0;
        }

        public override int VisitContinue_stmt(BRAQParser.Continue_stmtContext context)
        {
            Debug.Assert(loop_begin != null); //always true bc of checks
            il.Emit(OpCodes.Br_S, loop_begin.Value); 
            return 0;
        }

        public override int VisitDot_notation(BRAQParser.Dot_notationContext context)
        {
            if (context.basee == null) return context.single_name.Accept(this);
            MethodInfo mi = function_table[GetDeepToken(context.target)];
            if (!mi.IsStatic)
            {
                context.basee.Accept(this);    
            }
            context.target.Accept(this);
            return 0;
        }

        private IToken GetDeepToken(BRAQParser.CallContext context)
        {
            if (context.calee != null) return context.calee;
            return GetDeepToken(context.single);
        }

        private IToken GetDeepToken(BRAQParser.Short_callContext context)
        {
            if (context.calee != null) return context.calee;
            return context.single.var_node_.id_name;
        }

        public override int VisitCall(BRAQParser.CallContext context)
        {
            if (context.calee==null) return context.single.Accept(this);
            
            if (user_function_table.ContainsKey(context.calee))
            {
                var info = user_function_table[context.calee];
                
                foreach (var exprContext in context.expr())
                {
                    exprContext.Accept(this);
                }

                il.EmitCall(OpCodes.Call, info.method_builder, null);
                return 0;
            }
            
            var function_ptr = function_table[context.calee];
            
            foreach (var exprContext in context.expr())
            {
                exprContext.Accept(this);
            }

            if (function_ptr.IsStatic)
            {
                il.EmitCall(OpCodes.Call, function_ptr, null);
            }
            else
            {
                il.EmitCall(OpCodes.Call, function_ptr, null);
            }

            return 0;
        }
        
        public override int VisitVar_stmt(BRAQParser.Var_stmtContext context)
        {
            if (context.assignee != null)
            {
                context.assignee.Accept(this);
                
                var target_token = context.id_name;

                var assignment_point = variable_to_declaration[target_token];
                int var_id = Array.IndexOf(locals, assignment_point);
                if (var_id == -1) throw new Exception();
                //Console.WriteLine(var_id);
                //Console.WriteLine(_varList[0]);
                il.Emit(OpCodes.Stloc, var_id);
            }
            return 0;
        }

        public override int VisitExpr_stmt(BRAQParser.Expr_stmtContext context)
        {
            context.containing.Accept(this);
            if (type_dict[context.containing] != typeof(void))
            {
                il.Emit(OpCodes.Pop);
            }
            return 0;
        }

        public override int VisitAssign(BRAQParser.AssignContext context)
        {
            if (context.id_name != null)
            {
                context.assignee.Accept(this);
            
                
                
                var target_token = context.id_name;

                if (variable_to_declaration.ContainsKey(target_token))
                {
                    var assignment_point = variable_to_declaration[target_token];
            
                    int var_id = Array.IndexOf(locals, assignment_point);

                    il.Emit(OpCodes.Stloc, var_id);
                }
                int n = current_method.arguments.IndexOf(
                        current_method.arguments.Find(x => context.id_name.Text == x.a));
                    il.Emit(OpCodes.Starg, n);

                
            }
            else
            {
                context.assignee.Accept(this);
            }
            
            
            return 0;
        }

        public override int VisitLogical_or(BRAQParser.Logical_orContext context)
        {
            if (context.left != null)
            {
                context.left.Accept(this);
            
                //если левый истинный, тогда прыгаем, иначе правый
                
                Label truthey_or = il.DefineLabel(); //stack: left
                il.Emit(OpCodes.Brtrue_S, truthey_or); //stack:
                Label falsey_or = il.DefineLabel();

                context.right.Accept(this); //stack: right
                il.Emit(OpCodes.Br_S, falsey_or); //stack: right
                    
                il.MarkLabel(truthey_or); //from brtrue, stack: 
                il.Emit(OpCodes.Ldc_I4_1);// stack : 1
                    
                il.MarkLabel(falsey_or); //from br_s, stack: right
            }
            else
            {
                context.right.Accept(this);
            }
            
            return 0;
        }

        public override int VisitLogical_xor(BRAQParser.Logical_xorContext context)
        {
            if (context.left != null)
            {
                context.left.Accept(this);
                context.right.Accept(this);
                //вычисляются оба оператора
                // a xor b ~~~ a !=b ~~~ (a==b)==0
                
                il.Emit(OpCodes.Ceq);
                il.Emit(OpCodes.Ldc_I4_0);
                il.Emit(OpCodes.Ceq);
            }
            else
            {
                context.right.Accept(this);
            }
            return 0;
        }

        public override int VisitLogical_and(BRAQParser.Logical_andContext context)
        {
            if (context.left != null)
            {
                context.left.Accept(this);
                // если левый оператор ложный, тогда перепрыгиваем, иначе правый
                Label falsey = il.DefineLabel(); //stack: left
                il.Emit(OpCodes.Brfalse_S, falsey); //stack: 
                context.right.Accept(this); //stack: right
                Label truthey = il.DefineLabel();
                il.Emit(OpCodes.Br_S, truthey); //stack: right
                    
                il.MarkLabel(falsey); //from Brfalse, stack: 
                il.Emit(OpCodes.Ldc_I4_0); //stack: 0
                il.MarkLabel(truthey); //from br_s, stack: right
            }
            else
            {
                context.right.Accept(this);
            }
            return 0;
        }

        public override int VisitLogical_equal(BRAQParser.Logical_equalContext context)
        {
            if (context.left != null)
            {
                context.left.Accept(this);
                context.right.Accept(this);

                if (context.op.Text == "?=")
                {
                    if (type_dict[context.left] == typeof(string))
                    {
                        il.EmitCall(OpCodes.Call, typeof(string).GetMethod("op_Equality",
                            new[] {typeof(string), typeof(string)}) ?? throw new BindError(), null);
                    }
                    else
                    {
                        il.Emit(OpCodes.Ceq);
                    }
                }
                else
                {
                    if (type_dict[context.left] == typeof(string))
                    {
                        il.EmitCall(OpCodes.Call, typeof(string).GetMethod("op_Inequality",
                            new[] {typeof(string), typeof(string)}) ?? throw new BindError(), null);
                    }
                    else
                    {
                        il.Emit(OpCodes.Ceq);
                        il.Emit(OpCodes.Ldc_I4_0);
                        il.Emit(OpCodes.Ceq);
                    }
                }
            }
            else
            {
                context.right.Accept(this);
            }
            return 0;
        }

        public override int VisitLogical_gr_le(BRAQParser.Logical_gr_leContext context)
        {
            if (context.left != null)
            {
                context.left.Accept(this);
                context.right.Accept(this);
                switch (context.op.Text)
                {
                    case ">":
                        il.Emit(OpCodes.Cgt);
                        break;
                    case ">=": // a>=b  ~~~ !(a<b) ~~~ (a<b) == 0
                        il.Emit(OpCodes.Clt);
                        il.Emit(OpCodes.Ldc_I4_0);
                        il.Emit(OpCodes.Ceq);
                        break;
                    case "<":
                        il.Emit(OpCodes.Clt);
                        break;
                    case "<=":
                        il.Emit(OpCodes.Cgt);
                        il.Emit(OpCodes.Ldc_I4_0);
                        il.Emit(OpCodes.Ceq);
                        break;
                }
            }
            else
            {
                context.right.Accept(this);
            }
            return 0;
        }

        public override int VisitAddition(BRAQParser.AdditionContext context)
        {
            if (context.left != null)
            {
                context.left.Accept(this);
                context.right.Accept(this);
                switch (context.op.Text)
                {
                    case "+":
                        if (type_dict[context.left] == typeof(string))
                        {
                            il.EmitCall(OpCodes.Call, typeof(string).GetMethod("Concat",
                                new[] {typeof(string), typeof(string)}) ?? throw new BindError(), null);
                        }
                        else
                        {
                            il.Emit(OpCodes.Add);
                        }
                    
                        break;
                    case "-":
                        il.Emit(OpCodes.Sub);
                        break;
                }
            }
            else
            {
                context.right.Accept(this);
            }
            return 0;
        }

        public override int VisitMultiplication(BRAQParser.MultiplicationContext context)
        {
            if (context.left != null)
            {
                context.left.Accept(this);
                context.right.Accept(this);

                switch (context.op.Text)
                {
                    case "*":
                        il.Emit(OpCodes.Mul);
                        break;
                    case "/":
                        il.Emit(OpCodes.Div);
                        break;
                    case "%":
                        il.Emit(OpCodes.Rem);
                        break;
                }
                
            }
            else
            {
                context.right.Accept(this);
            }
            return 0;
        }

        public override int VisitUnary_not_neg(BRAQParser.Unary_not_negContext context)
        {
            context.right.Accept(this);
            if (context.op != null)
            {
                switch (context.op.Text)
                {
                    case "-":
                        il.Emit(OpCodes.Neg);
                        break;
                    case "not":
                        il.Emit(OpCodes.Ldc_I4_0);
                        il.Emit(OpCodes.Ceq);
                        break;
                }
            }

            return 0;
        }

        public override int VisitShort_call(BRAQParser.Short_callContext context)
        {
            if (context.calee==null) return context.single.Accept(this);
            //one of
            context.arg.Accept(this);

            if (user_function_table.ContainsKey(context.calee))
            {
                
                var info = user_function_table[context.calee];

                il.EmitCall(OpCodes.Call, info.method_builder, null);
                return 0;
                
            }

            var function_ptr = function_table[context.calee];
            
            il.EmitCall(function_ptr.IsStatic ? OpCodes.Call : OpCodes.Calli, function_ptr, null);

            return 0;
        }

        public override int VisitFunction_def_stmt(BRAQParser.Function_def_stmtContext context)
        {
            if (current_method == null)
            {
                Console.WriteLine("Huh!?");
                return 0;
            }

            method_end = il.DefineLabel();

            foreach (var l in locals)
            {
                il.DeclareLocal(typerResult.local_types[l]);
            }
            
            if (current_method.return_type!=typeof(void))
            {
                return_holder = il.DeclareLocal(current_method.return_type);
            }
            
            context.function_body.Accept(this);
            
            
            il.MarkLabel(method_end.Value);
            
            if (current_method.return_type!=typeof(void))
            {
                il.Emit(OpCodes.Ldloc, return_holder);
            }
            
            il.Emit(OpCodes.Ret);
            return 0;
        }

        public override int VisitReturn_stmt(BRAQParser.Return_stmtContext context)
        {
            if (current_method == null)
            {
                Console.WriteLine("Huh!?");
                throw new Exception();
            }

            if (context.return_value != null)
            {
                context.return_value.Accept(this);
                il.Emit(OpCodes.Stloc, return_holder);
            }
            il.Emit(OpCodes.Br_S, method_end.Value);
            return 0;
        }
    }
}