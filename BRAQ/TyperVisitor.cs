using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;

namespace BRAQ
{
    //TODO
    public class TyperVisitor : BRAQParserBaseVisitor<Dictionary<ParserRuleContext, Type>>
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

        private Dictionary<ParserRuleContext, Type> dict;

        public Dictionary<IToken, MethodInfo> function_table = new Dictionary<IToken, MethodInfo>();
        
        private List<TypeHelper> TypeAllowances;
        
        
        //NEW
        // для каждого упоминания переменной найдём точку объявления
        private Dictionary<IToken, BRAQParser.Var_stmtContext> variable_to_declaration;
        //для каждого объявления найдём тип
        private Dictionary<BRAQParser.Var_stmtContext, Type> declaration_to_type;


        public struct TyperResult
        {
            public Dictionary<ParserRuleContext, Type> expr_type;
            public Dictionary<IToken, MethodInfo> outer_function_table;
            public Dictionary<IToken, MethodInfo> user_function_table; // yet unused
        }
        
        

        public static TyperResult solveTypes(BRAQParser.ProgramContext context)
        {
            //check that there are no unassigned variables
            var assigners = AssignCheckVisitor.getAssigners(context);


            //make types
            var visitor = new TyperVisitor(assigners);
            context.Accept(visitor);
            var res = new TyperResult();
            res.expr_type = visitor.dict;
            res.outer_function_table = visitor.function_table;
            return res;
        }
        
        
        private TyperVisitor(List<Pair<string, ParserRuleContext>> assigners)
        {
            //varname_type_dict = new Dictionary<string, Type>();
            dict = new Dictionary<ParserRuleContext, Type>();
            this.assigners = assigners;
            varname_dict = new Dictionary<string, BRAQParser.Var_stmtContext>();

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


        public override Dictionary<ParserRuleContext, Type> VisitProgram(BRAQParser.ProgramContext context)
        {
            base.VisitProgram(context);
            return dict;
        }

        public override Dictionary<ParserRuleContext, Type> VisitIf_stmt(BRAQParser.If_stmtContext context)
        {
            context.cond.Accept(this);
            if (dict[context.cond]!=typeof(bool))
            {
                string msg =
                    $"expected boolean type but got {dict[context.cond]} in if condition [Line {context.cond.Start.Line}]";
                Console.WriteLine(msg);
                
                throw new TypeMismatchError(msg);
                
            }
            
            context.then_branch.Accept(this);
            context.else_branch?.Accept(this);

            return null;
        }

        public override Dictionary<ParserRuleContext, Type> VisitVar_stmt(BRAQParser.Var_stmtContext context)
        {
            if (context.assignee != null)
            {
                context.assignee.Accept(this);
                varname_dict[context.id_name.Text] = context;
                dict[context] = dict[context.assignee];
            }
            else
            {
                var assigner = assigners.Where(x => x.a == context.id_name.Text).Single().b;
                assigner.Accept(this);
                varname_dict[context.id_name.Text] = context;
                dict[context] = dict[assigner];

            }
            return null;
        }

        public override Dictionary<ParserRuleContext, Type> VisitAssign(BRAQParser.AssignContext context)
        {
            if ()
            return null;
        }

        public override Dictionary<ParserRuleContext, Type> VisitLogical_or(BRAQParser.Logical_orContext context)
        {
            return null;
        }

        public override Dictionary<ParserRuleContext, Type> VisitLogical_xor(BRAQParser.Logical_xorContext context)
        {
            return null;
        }

        public override Dictionary<ParserRuleContext, Type> VisitLogical_and(BRAQParser.Logical_andContext context)
        {
            return null;
        }

        public override Dictionary<ParserRuleContext, Type> VisitLogical_equal(BRAQParser.Logical_equalContext context)
        {
            return null;
        }

        public override Dictionary<ParserRuleContext, Type> VisitLogical_gr_le(BRAQParser.Logical_gr_leContext context)
        {
            return null;
        }

        public override Dictionary<ParserRuleContext, Type> VisitAddition(BRAQParser.AdditionContext context)
        {
            return null;
        }

        public override Dictionary<ParserRuleContext, Type> VisitMultiplication(BRAQParser.MultiplicationContext context)
        {
            return null;
        }

        public override Dictionary<ParserRuleContext, Type> VisitShort_call(BRAQParser.Short_callContext context)
        {
            return null;
        }

        /*public override Dictionary<ParserRuleContext, Type> VisitVar_stmt_base(BRAQParser.Var_stmt_baseContext context)
        {
            if (context.assignee != null)
            {
                context.assignee.Accept(this);
                varname_dict[context.id_name.Text] = context;
                dict[context] = dict[context.assignee];
            }
            else
            {
                var assigner = assigners.Where(x => x.a == context.id_name.Text).Single().b;
                assigner.Accept(this);
                varname_dict[context.id_name.Text] = context;
                dict[context] = dict[assigner];

            }
            return base.VisitVar_stmt_base(context);
        }*/
        

        public override Dictionary<ParserRuleContext, Type> VisitAssign_stmt_base(BRAQParser.Assign_stmt_baseContext context)
        {
            base.VisitAssign_stmt_base(context);
            context.assignee.Accept(this);
            if (dict[varname_dict[context.id_name.Text]] != dict[context.assignee])
            {
                string msg =
                    $"assigning {dict[context.assignee]} to variable {context.id_name.Text} of type {dict[varname_dict[context.id_name.Text]]}";
                Console.WriteLine(msg);
                throw new TypeMismatchError();
            }
            dict[context] = dict[context.assignee];
            return null;
        }

        public override Dictionary<ParserRuleContext, Type> VisitRead_stmt_base(BRAQParser.Read_stmt_baseContext context)
        {
            dict[context] = typeof(int);
            
            
            return null;
        }

        public override Dictionary<ParserRuleContext, Type> VisitExpr(BRAQParser.ExprContext context)
        {
            if (context.grouping != null)
            {
                context.grouping.Accept(this);
                dict[context] = dict[context.grouping];
            }else if (context.num != null)
            {
                context.num.Accept(this);
                dict[context] = dict[context.num];
            }else if (context.call_exr!=null)
            {
                context.call_exr.Accept(this);
                dict[context] = dict[context.call_exr];
            }
            else if (context.unary_not_op != null)
            {
                context.right.Accept(this);
                var right_type = dict[context.right];
                try
                {
                    Console.WriteLine(context.unary_not_op.Text);
                    Console.WriteLine(right_type);
                    
                    var type_pair = TypeAllowances
                        .Find(x => 
                            x.Equals(new TypeHelper(null, right_type, context.unary_not_op.Text, null)));
                    dict[context] = type_pair.result;
                    Console.WriteLine(type_pair.result);
                }
                catch(ArgumentNullException )
                {
                    string msg = $"Failed to find matching operator {context.unary_not_op.Text} for type {right_type} [Line {context.unary_not_op.Line}]";
                    Console.WriteLine(msg);
                    throw new TypeMismatchError(msg);
                }
            }
            else
            {
                context.left.Accept(this);
                context.right.Accept(this);
                var left_type = dict[context.left];
                var right_type = dict[context.right];

                if (left_type != right_type)
                {
                    string msg = $"different left and right types: {left_type} {right_type} for operator {context.op.Text} [Line {context.op.Line}";
                    Console.WriteLine(msg);
                    throw new TypeMismatchError(msg);
                }
                try
                {
                    Console.WriteLine(left_type);
                    Console.WriteLine(context.op.Text);
                    Console.WriteLine(right_type);
                    var type_pair = TypeAllowances
                        .Find(x => 
                            x.Equals(new TypeHelper(left_type, right_type, context.op.Text, null)));
                    dict[context] = type_pair.result;
                    Console.WriteLine(type_pair.result);
                }
                catch(ArgumentNullException )
                {
                    string msg =
                        $"Failed to find operator {context.op.Text} for types {left_type} {right_type} [Line {context.op.Line}]";
                    Console.WriteLine(msg);
                    throw new TypeMismatchError(msg);
                }
            }

            return null;
        }

        public override Dictionary<ParserRuleContext, Type> VisitGroup(BRAQParser.GroupContext context)
        {
            context.containing.Accept(this);
            dict[context] = dict[context.containing];
            return null;
        }

        public override Dictionary<ParserRuleContext, Type> VisitCall_or_literal(BRAQParser.Call_or_literalContext context)
        {
            if (context.containing_call != null)
            {
                context.containing_call.Accept(this);
                dict[context] = dict[context.containing_call];
            }
            else
            {
                context.containing_literal.Accept(this);
                dict[context] = dict[context.containing_literal];
            }
            return null;
        }

        public override Dictionary<ParserRuleContext, Type> VisitCall(BRAQParser.CallContext context)
        {
            //TODO
            //get argument list
            Type[] types;
            if (context.single_argument != null)
            {
                context.single_argument.Accept(this);
                types = new[] { dict[context.single_argument]};
            }
            else
            {

                
                
                foreach (var exprContext in context.multiple_arguments.expr())
                {
                    exprContext.Accept(this);
                }

                types = context.multiple_arguments.expr().Select(x => dict[x]).ToArray();
            }

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
                dict[context] = predef_function_info.ReturnType;
            }
            else
            {
                Console.WriteLine(function_token.Text);
                Console.WriteLine(String.Join(" ", types.ToList().Select(x => x.ToString())) );
                throw new BindError();
            }
            
            return null;
        }

        
        public override Dictionary<ParserRuleContext, Type> VisitArg_list(BRAQParser.Arg_listContext context)
        {
            foreach (var s in context.expr())
            {
                s.Accept(this);
            }
            
            return null;
        }
        

        public override Dictionary<ParserRuleContext, Type> VisitLiteral(BRAQParser.LiteralContext context)
        {
            if (context.num != null)
            {
                dict[context] = typeof(int);
            }
            else if (context.str != null)
            {
                dict[context] = typeof(string);
            }
            
            else if (context.var_node_!=null)
            {
                context.var_node_.Accept(this);
                dict[context] = dict[context.var_node_];
            }
            else if (context.double_num != null)
            {
                dict[context] = typeof(double);
            }

            return null;
        }

        public override Dictionary<ParserRuleContext, Type> VisitVar_node(BRAQParser.Var_nodeContext context)
        {
            string var_name = context.id_name.Text;
            var assigner = assigners.Find(x => x.a == var_name).b;
            assigner.Accept(this);
            dict[context] = dict[assigner];

            return null;
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