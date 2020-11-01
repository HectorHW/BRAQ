using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Antlr4.Runtime;
using Antlr4.Runtime.Dfa;
using Antlr4.Runtime.Misc;

namespace BRAQ
{
    public class ILVisitor : BRAQParserBaseVisitor<int>
    {
        private ILGenerator il;
        private Dictionary<IToken, BRAQParser.Var_stmt_baseContext> dict;
        private readonly ArrayList<BRAQParser.Var_stmt_baseContext> _varList;
        
        private LocalBuilder for_print;
        private Dictionary<ParserRuleContext, Type> type_dict;

        private Dictionary<IToken, MethodInfo> function_table;

        public ILVisitor(ILGenerator il, 
            Dictionary<IToken, BRAQParser.Var_stmt_baseContext> dict, 
            ArrayList<BRAQParser.Var_stmt_baseContext> var_list, 
            Dictionary<ParserRuleContext, Type> type_dict,
            Dictionary<IToken, MethodInfo> function_table)
        {
            this.il = il;
            this.dict = dict;
            _varList = var_list;
            this.type_dict = type_dict;
            this.function_table = function_table;
        }

        public override int VisitArg_list(BRAQParser.Arg_listContext context)
        {
            return base.VisitArg_list(context);
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
            else
            {
                context.var_node_.Accept(this); //variable
            }
            return 0;
        }

        public override int VisitVar_node(BRAQParser.Var_nodeContext context)
        {
            dict.TryGetValue(context.id_name, out var var_decl);
            int var_id = _varList.IndexOf(var_decl);
            il.Emit(OpCodes.Ldloc, var_id);
            return 0;
        }

        public override int VisitRead_stmt_base(BRAQParser.Read_stmt_baseContext context)
        {
            //call read
            il.EmitCall(OpCodes.Call, typeof(Console).GetMethod("ReadLine"), null);
            
            //call toInt
            il.EmitCall(OpCodes.Call, typeof(int).GetMethod("Parse", new[]{typeof(string)}), null);
            //save to var
            dict.TryGetValue(context.arg.id_name, out var var_decl);
            int var_id = _varList.IndexOf(var_decl);
            il.Emit(OpCodes.Stloc, var_id);
            return 0;
        }

        public override int VisitExpr(BRAQParser.ExprContext context)
        {
            if (context.ChildCount == 1)
            {
                context.GetChild(0).Accept(this);
                return 0;
            }
            
            if (context.unary_not_op != null)
            {
                context.right.Accept(this);
                il.Emit(OpCodes.Ldc_I4_0);
                il.Emit(OpCodes.Ceq);

                return 0;
            }
            
            
            //two -> binary op
            
            context.left.Accept(this);
            if (context.op.Text !="and" && context.op.Text !="or")
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
                case "*":
                    il.Emit(OpCodes.Mul);
                    break;
                case "/":
                    il.Emit(OpCodes.Div);
                    break;
                case "%":
                    il.Emit(OpCodes.Rem);
                    break;
                
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

                case "?=":
                    if (type_dict[context.left] == typeof(string))
                    {
                        il.EmitCall(OpCodes.Call, typeof(string).GetMethod("op_Equality",
                            new[] {typeof(string), typeof(string)}) ?? throw new BindError(), null);
                    }
                    else
                    {
                        il.Emit(OpCodes.Ceq);
                    }

                    break;
                case "!=":
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
                    break;
                
                //Short-circuit
                case "and":
                    // если левый оператор ложный, тогда перепрыгиваем, иначе правый
                    //il.Emit(OpCodes.Ldc_I4_0);
                    Label falsey = il.DefineLabel(); //stack: left
                    il.Emit(OpCodes.Brfalse_S, falsey); //stack: 
                    context.right.Accept(this); //stack: right
                    Label truthey = il.DefineLabel();
                    il.Emit(OpCodes.Br_S, truthey); //stack: right
                    
                    il.MarkLabel(falsey); //from Brfalse, stack: 
                    il.Emit(OpCodes.Ldc_I4_0); //stack: 0
                    il.MarkLabel(truthey); //from br_s, stack: right
                    break;
                
                case "or":
                    //если левый истинный, тогда прыгаем, иначе правый
                
                    Label truthey_or = il.DefineLabel(); //stack: left
                    il.Emit(OpCodes.Brtrue_S, truthey_or); //stack:
                    Label falsey_or = il.DefineLabel();

                    context.right.Accept(this); //stack: right
                    il.Emit(OpCodes.Br_S, falsey_or); //stack: right
                    
                    il.MarkLabel(truthey_or); //from brtrue, stack: 
                    il.Emit(OpCodes.Ldc_I4_1);// stack : 1
                    
                    il.MarkLabel(falsey_or); //from br_s, stack: right

                    break;
                
                case "xor":
                    //вычисляются оба оператора
                    // a xor b ~~~ a !=b ~~~ (a==b)==0
                    il.Emit(OpCodes.Ceq);
                    il.Emit(OpCodes.Ldc_I4_0);
                    il.Emit(OpCodes.Ceq);
                    break;
                    
            }

            return 0;
        }

        public override int VisitGroup(BRAQParser.GroupContext context)
        {
            context.containing.Accept(this);
            return 0;
        }

        public override int VisitCall(BRAQParser.CallContext context)
        {
            var function_ptr = function_table[context.calee];
            context.arguments.Accept(this);

            il.EmitCall(OpCodes.Call, function_ptr, null);
            return 0;
        }

        public override int VisitAssign_stmt_base(BRAQParser.Assign_stmt_baseContext context)
        {
            
            context.assignee.Accept(this);
            

            var target_token = context.id_name;
            
            dict.TryGetValue(target_token, out var var_decl);
            int var_id = _varList.IndexOf(var_decl);

            il.Emit(OpCodes.Stloc, var_id);
            
            
            return 0;
        }

        public override int VisitPrint_stmt_base(BRAQParser.Print_stmt_baseContext context)
        {
            context.arg.Accept(this);

            var T = type_dict[context.arg];
            
            il.EmitCall(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new[]{T}), null);
            
            //il.Emit(OpCodes.Stloc, for_print); // var_i = stack[0]
            
            //il.EmitWriteLine(for_print); // print(var_i)
            
            return 0;
        }

        public override int VisitVar_stmt_base(BRAQParser.Var_stmt_baseContext context)
        {
            if (context.assignee != null)
            {
                context.assignee.Accept(this);
                
                var target_token = context.id_name;
            
                dict.TryGetValue(target_token, out var var_decl);
                int var_id = _varList.IndexOf(var_decl);
                if (var_id == -1) throw new Exception();
                //Console.WriteLine(var_id);
                //Console.WriteLine(_varList[0]);
                il.Emit(OpCodes.Stloc, var_id);
            }

            return 0;
        }

        public override int VisitProgram(BRAQParser.ProgramContext context)
        {
            //define variables
            for (int i = 0; i < _varList.Count; i++)
            {
                //il.DeclareLocal(typeof(int));
                var T = type_dict[_varList[i]];
                il.DeclareLocal(T);
            }
            
            for_print = il.DeclareLocal(typeof(int));

            base.VisitProgram(context);

            il.Emit(OpCodes.Ret);
            
            return 0;
        }
    }
}