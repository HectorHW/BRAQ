using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;

namespace BRAQ
{
    //TODO
    public class TyperVisitor : BRAQParserBaseVisitor<Type>
    {

        class TypeHelper
        {
            public Type left;
            public Type right;
            public string op;
            public Type result;

            public TypeHelper(Type left, Type right, string op, Type result)
            {
                this.left = left;
                this.right = right;
                this.op = op;
                this.result = result;
            }

            public override bool Equals(object obj)
            {
                if (obj.GetType() != GetType() )
                {
                    return false;
                }

                var obj_ = (TypeHelper) obj;
                return left == obj_.left && right == obj_.right && op == obj_.op;
            }
        }

        private Dictionary<ParserRuleContext, Type> type_dict;

        public Dictionary<IToken, MethodInfo> function_table;
        
        private List<TypeHelper> TypeAllowances;
        
        
        //NEW
        // для каждого упоминания переменной найдём точку объявления
        private Dictionary<IToken, BRAQParser.Var_stmtContext> variable_to_declaration;
        //для каждого объявления найдём тип
        private Dictionary<BRAQParser.Var_stmtContext, Type> declaration_to_type;

        private Dictionary<BRAQParser.Var_stmtContext, ParserRuleContext> declaration_to_assignment;

        public struct TyperResult
        {
            public Dictionary<ParserRuleContext, Type> expr_type; // including var_def
            public Dictionary<IToken, MethodInfo> outer_function_table;
            public Dictionary<IToken, MethodInfo> user_function_table; // yet unused
        }
        
        

        public static TyperResult solveTypes(BRAQParser.ProgramContext context, AssignCheckVisitor.AssignCheckResult result)
        {
            //make types
            var visitor = new TyperVisitor(result);
            context.Accept(visitor);
            var res = new TyperResult();
            res.expr_type = visitor.type_dict;
            res.outer_function_table = visitor.function_table;
            return res;
        }

        public override Type VisitExpr(BRAQParser.ExprContext context)
        {
            return type_dict[context] = context.containing.Accept(this);
        }

        private TyperVisitor(AssignCheckVisitor.AssignCheckResult assigners)
        {
            variable_to_declaration = assigners.token_to_def;
            declaration_to_type = new Dictionary<BRAQParser.Var_stmtContext, Type>();
            type_dict = new Dictionary<ParserRuleContext, Type>();
            function_table = new Dictionary<IToken, MethodInfo>();
            declaration_to_assignment = assigners.def_to_assign;
            
            

            TypeAllowances = File.ReadAllText("binary_typing.txt")
                .Split(new[] {'\n', '\r'}, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries))
                .Select(x =>
                    new TypeHelper(Type.GetType(x[0]), Type.GetType(x[2]), x[1], Type.GetType(x[4]))
                ).ToList()
                
                .Concat<TypeHelper>(
                    
                    
                    
                    File.ReadAllText("unary_typing.txt")
                        .Split(new[] {'\n', '\r'}, StringSplitOptions.RemoveEmptyEntries)
                        .Select(x => x.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries))
                        .Select(x =>
                            new TypeHelper(null, Type.GetType(x[0]), x[1], Type.GetType(x[3]))
                        ).ToList()
                    
                    
                    
                    ).ToList();

        }

        public override Type VisitIf_stmt(BRAQParser.If_stmtContext context)
        {
            if ( context.cond.Accept(this)!=typeof(bool))
            {
                string msg =
                    $"expected boolean type but got {type_dict[context.cond]} in if condition [Line {context.cond.Start.Line}]";
                Console.WriteLine(msg);
                
                throw new TypeMismatchError(msg);
                
            }
            
            context.then_branch.Accept(this);
            context.else_branch?.Accept(this);

            return null;
        }

        public override Type VisitVar_stmt(BRAQParser.Var_stmtContext context)
        {
            if (context.assignee != null)
            {
                type_dict[context] = context.assignee.Accept(this);
                declaration_to_type[context] = type_dict[context];
            }
            else
            {
                var assigner = declaration_to_assignment[context];
                Type t = assigner.Accept(this);
                type_dict[context] = t;
                declaration_to_type[context] = t;

            }
            return null;
        }

        public override Type VisitAssign(BRAQParser.AssignContext context)
        {
            Type right_type = context.assignee.Accept(this);
            if (context.id_name != null)
            {
                Type target_type = declaration_to_type[variable_to_declaration[context.id_name]];
                //check types
                if (target_type != right_type)
                {
                    Console.WriteLine(
                        $"type mismatch: assigned {right_type} to variable {context.id_name.Text} of type {target_type} [Line {context.id_name.Line}]");
                    throw new TypeMismatchError();
                }
            }
            
            return type_dict[context] = typeof(void);
        }
        #region boring_binary
        public override Type VisitLogical_or(BRAQParser.Logical_orContext context)
        {
            return binary_optional_left_expr(context, context.left, context.op, context.right);
        }

        public override Type VisitLogical_xor(BRAQParser.Logical_xorContext context)
        {
            return binary_optional_left_expr(context, context.left, context.op, context.right);
        }

        public override Type VisitLogical_and(BRAQParser.Logical_andContext context)
        {
            return binary_optional_left_expr(context, context.left, context.op, context.right);
        }

        public override Type VisitLogical_equal(BRAQParser.Logical_equalContext context)
        {
            return binary_optional_left_expr(context, context.left, context.op, context.right);
        }

        public override Type VisitLogical_gr_le(BRAQParser.Logical_gr_leContext context)
        {
            return binary_optional_left_expr(context, context.left, context.op, context.right);
        }

        public override Type VisitAddition(BRAQParser.AdditionContext context)
        {
            return binary_optional_left_expr(context, context.left, context.op, context.right);
        }
        

        public override Type VisitMultiplication(BRAQParser.MultiplicationContext context)
        {
            return binary_optional_left_expr(context, context.left, context.op, context.right);
        }
        #endregion
        
        public override Type VisitUnary_not_neg(BRAQParser.Unary_not_negContext context)
        {
            //right: right_short_call=short_call | right_call=call | right_literal=literal
            Type right_type;
            if (context.right_short_call != null)
            {
                right_type = context.right_short_call.Accept(this);
            }
            else if (context.right_call != null)
            {
                right_type = context.right_call.Accept(this);
            }
            else if (context.right_literal != null)
            {
                right_type = context.right_literal.Accept(this);
            }
            else
            {
                throw new NotImplementedException();
            }

            if (context.op != null)
            {
                try
                {
                    Console.WriteLine(context.op.Text);
                    Console.WriteLine(right_type);
                    
                    var type_pair = TypeAllowances
                        .Find(x => 
                            x.Equals(new TypeHelper(null, right_type, context.op.Text, null)));
                    type_dict[context] = type_pair.result;
                    Console.WriteLine(type_pair.result);
                    return type_dict[context];
                }
                catch(ArgumentNullException )
                {
                    string msg = $"Failed to find matching operator {context.op.Text} for type {right_type} [Line {context.op.Line}]";
                    Console.WriteLine(msg);
                    throw new TypeMismatchError(msg);
                }  
            }

            type_dict[context] = right_type;
            return right_type;


        }

        public override Type VisitShort_call(BRAQParser.Short_callContext context)
        {
            //TODO user_defined functions
            
            //get argument type
            Type argument_type;
            if (context.c_arg != null)
            {
                argument_type = context.c_arg.Accept(this);
            }else if (context.sc_arg != null)
            {
                argument_type = context.sc_arg.Accept(this);
            }else if (context.l_arg != null)
            {
                argument_type = context.l_arg.Accept(this);
            }
            else
            {
                throw new NotImplementedException();
            }

            Type[] types = {argument_type};
            IToken function_token = context.calee;
            
            MethodInfo predef_function_info = PredefsHelper.Resolve(function_token.Text, types);
            
            if (predef_function_info != null)
            {

                if (predef_function_info.GetParameters()
                    .Zip(types, (r, w) => new KeyValuePair<Type, Type>(r.ParameterType, w))
                    .Any(p => p.Key != p.Value))
                {
                    Console.WriteLine("could not bind {0}({1}) [Line {2}]", 
                        function_token.Text, 
                        String.Join(" ", types.Select(x => x.ToString())),
                        function_token.Line
                    );
                    throw new BindError();
                }
                
                function_table[function_token] = predef_function_info;
                type_dict[context] = predef_function_info.ReturnType;
            }
            else
            {
                Console.WriteLine(function_token.Text);
                Console.WriteLine(String.Join(" ", types.ToList().Select(x => x.ToString())) );
                throw new BindError();
            }

            return predef_function_info.ReturnType;
            
        }
        
        public override Type VisitGroup(BRAQParser.GroupContext context)
        {
            context.containing.Accept(this);
            
            return type_dict[context] = type_dict[context.containing];
        }
        
        public override Type VisitCall(BRAQParser.CallContext context)
        {
            //TODO
            //get argument list

            
                
            foreach (var exprContext in context.expr())
            {
                exprContext.Accept(this);
            }
            Console.WriteLine(context.expr().Length);
            Type[] types = context.expr().Select(x => type_dict[x]).ToArray();
            

            IToken function_token = context.calee;

            

            //MethodInfo predef_function_info = typeof(Predefs).GetMethod(function_token.Text, types);
            MethodInfo predef_function_info = PredefsHelper.Resolve(function_token.Text, types);
            
            if (predef_function_info != null)
            {

                if (predef_function_info.GetParameters()
                    .Zip(types, (r, w) => new KeyValuePair<Type, Type>(r.ParameterType, w))
                    .Any(p => p.Key != p.Value))
                {
                    Console.WriteLine("could not bind {0}({1}) [Line {2}]", 
                        function_token.Text, 
                        String.Join(" ", types.Select(x => x.ToString())),
                        function_token.Line
                        );
                    throw new BindError();
                }
                
                function_table[function_token] = predef_function_info;
                type_dict[context] = predef_function_info.ReturnType;
                return predef_function_info.ReturnType;
            }

            Console.WriteLine(function_token.Text);
            Console.WriteLine(String.Join(" ", types.ToList().Select(x => x.ToString())) );
            throw new BindError();
        }
        
        

        public override Type VisitLiteral(BRAQParser.LiteralContext context)
        {
            if (context.num != null)
            {
                return type_dict[context] = typeof(int);
            }

            if (context.str != null)
            {
                return type_dict[context] = typeof(string);
            }

            if (context.var_node_!=null)
            {
                
               return type_dict[context] = context.var_node_.Accept(this);
            }

            if (context.double_num != null)
            {
                return type_dict[context] = typeof(double);
            }

            if (context.containing_group != null)
            {
                return type_dict[context] = context.containing_group.Accept(this);
            }

            throw new NotImplementedException();
        }

        public override Type VisitWhile_loop_stmt(BRAQParser.While_loop_stmtContext context)
        {
            if (context.cond!=null && context.cond.Accept(this) != typeof(bool))
            {
                Console.WriteLine($"Expected {typeof(bool)} in loop condition but got {type_dict[context.cond]} [Line {context.cond.Start.Line}]");
                throw new TypeMismatchError();
            }
            return context.body.Accept(this);
        }

        public override Type VisitVar_node(BRAQParser.Var_nodeContext context)
        {
            var def_point = variable_to_declaration[context.id_name];
            Type t = declaration_to_type[def_point];
            type_dict[context] = t;
            return t;
        }


        private Type get_binary_operator_result(Type left_type, Type right_type, IToken op)
        {
            if (left_type != right_type)
            {
                string msg = $"different left and right types: {left_type} {right_type} for operator {op.Text} [Line {op.Line}]";
                Console.WriteLine(msg);
                throw new TypeMismatchError(msg);
            }
            try
            {
                Console.WriteLine(left_type);
                Console.WriteLine(op.Text);
                Console.WriteLine(right_type);
                var type_pair = TypeAllowances
                    .Find(x => 
                        x.Equals(new TypeHelper(left_type, right_type, op.Text, null)));
                Console.WriteLine(type_pair.result);
                return type_pair.result;
            }
            catch(ArgumentNullException )
            {
                string msg =
                    $"Failed to find operator {op.Text} for types {left_type} {right_type} [Line {op.Line}]";
                Console.WriteLine(msg);
                throw new TypeMismatchError(msg);
            }
        }

        private Type binary_optional_left_expr(ParserRuleContext context, ParserRuleContext left, IToken op, ParserRuleContext right)
        {
            if (left == null)
            {
                Type t = right.Accept(this);
                type_dict[context] = t;
                return t;
            }

            Type left_type = left.Accept(this);
            Type right_type = right.Accept(this);
            Type resulting_type = get_binary_operator_result(left_type, right_type, op);

            type_dict[context] = resulting_type;
            Debug.Assert(resulting_type != null && resulting_type != typeof(void));
            return resulting_type;

        }
    }


    public class TypeMismatchError : Exception
    {
        public string msg { get; set; }

        public TypeMismatchError(string msg)
        {
            this.msg = msg;
        }

        public TypeMismatchError()
        {
            this.msg = "";
        }
    }

    public class BindError : Exception
    {
    }

    public class TypesolvingError : Exception
    {
       /* public Dictionary<string, bool> dict;
        public TypesolvingError(Dictionary<string, bool> assigned)
        {
            dict = assigned;
        }*/
    }
}