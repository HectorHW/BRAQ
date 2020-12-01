using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
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

        public class OwnMethodInfo
        {
            public string name;
            public List<Pair<string, Type>> arguments;
            public Type return_type;
            public BRAQParser.Function_def_stmtContext body;
            public MethodBuilder method_builder = null;
            
            public OwnMethodInfo(string name, List<Pair<string, Type>> arguments, Type returnType, BRAQParser.Function_def_stmtContext body)
            {
                this.name = name;
                this.arguments = arguments;
                return_type = returnType;
                this.body = body;
            }
        }
        
        private Dictionary<ParserRuleContext, Type> type_dict;

        private TyperResult result_box;
        
        
        private List<TypeHelper> TypeAllowances;
        
        private OwnMethodInfo current_method = null;

        private Dictionary<BRAQParser.Var_stmtContext, Type> locals_type;
        private Dictionary<BRAQParser.Var_stmtContext, ParserRuleContext> local_def_to_assignment;
        private Dictionary<IToken, BRAQParser.Var_stmtContext> var_to_local_def;

        private List<OwnMethodInfo> user_functions;
        
        public class TyperResult
        {
            public Dictionary<ParserRuleContext, Type> type_dict = new Dictionary<ParserRuleContext, Type>(); // including var_def
            public Dictionary<IToken, MethodInfo> token_to_outer_function = new Dictionary<IToken, MethodInfo>();
            public Dictionary<IToken, OwnMethodInfo> token_to_user_function = new Dictionary<IToken, OwnMethodInfo>();

            public List<BRAQParser.Var_stmtContext> local_defs = new List<BRAQParser.Var_stmtContext>(); 

            public Dictionary<IToken, BRAQParser.Var_stmtContext> var_to_def;
            public List<IToken> argvars;
            public Dictionary<string, Type> argtypes = new Dictionary<string, Type>();
            public Dictionary<BRAQParser.Var_stmtContext, Type> local_types = new Dictionary<BRAQParser.Var_stmtContext, Type>();
            
            public OwnMethodInfo methodInfo = null;
            
            public TyperResult(Dictionary<IToken, BRAQParser.Var_stmtContext> varToDef, List<IToken> argvars)
            {
                var_to_def = varToDef;
                this.argvars = argvars;
            }
        }
        
        

        public static Dictionary<BRAQParser.Function_def_stmtContext, TyperResult>
            solveTypes(BRAQParser.ProgramContext context, 
                Dictionary<BRAQParser.Function_def_stmtContext, AssignCheckVisitor.AssignCheckResult> step1_result)
        {
            var answ = new Dictionary<BRAQParser.Function_def_stmtContext, TyperResult>();
            var user_functions = new List<OwnMethodInfo>();
            //function signatures
            foreach (var fdef in step1_result)
            {
                var tr = new TyperResult(fdef.Value.token_to_def, fdef.Value.token_to_arg.Keys.ToList());

                tr.methodInfo = new OwnMethodInfo(
                    fdef.Key.id_name.Text, 
                    fdef.Key.typed_id().Select(x=> new Pair<string, Type>(x.id_name.Text,string_to_type(x.type_name.Text))).ToList(), 
                    fdef.Key.of_type!=null ? string_to_type(fdef.Key.of_type.Text): typeof(void),
                    fdef.Key
                    );

                user_functions.Add(tr.methodInfo);
                
                tr.argtypes = tr.methodInfo.arguments.ToDictionary(x=>x.a, x=>x.b);
                
                tr.argvars = fdef.Value.token_to_arg.Keys.ToList();
                
                answ[fdef.Key] = tr;
            }

            foreach (var fdef in step1_result)
            {
                var v = new TyperVisitor(answ[fdef.Key], fdef.Value.def_to_assign, user_functions);
                fdef.Key.Accept(v);
            }

            return answ;
        }

        public override Type VisitFunction_def_stmt(BRAQParser.Function_def_stmtContext context)
        {
            context.function_body.Accept(this);
            return result_box.methodInfo.return_type;
        }

        public override Type VisitTyped_id(BRAQParser.Typed_idContext context)
        {
            return type_dict[context] = string_to_type(context.type_name.Text);
        }

        public override Type VisitExpr(BRAQParser.ExprContext context)
        {
            return type_dict[context] = context.containing.Accept(this);
        }

        private TyperVisitor(TyperResult result_box,
            Dictionary<BRAQParser.Var_stmtContext, ParserRuleContext> assignment_points,
            List<OwnMethodInfo> user_functions)
        {
            this.result_box = result_box;
            type_dict = result_box.type_dict;
            var_to_local_def = result_box.var_to_def;
            local_def_to_assignment = assignment_points;
            this.user_functions = user_functions;
            locals_type = result_box.local_types;

            #region typecombos_reading
            this.TypeAllowances = File.ReadAllText("binary_typing.txt")
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
            #endregion
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

        public override Type VisitVar_stmt(BRAQParser.Var_stmtContext context) //var a (=...)?
        {
            var assigner = this.local_def_to_assignment[context];
            result_box.local_defs.Add(context);
            
            Type t = assigner.Accept(this);
            locals_type[context] = t;

            return type_dict[context] = typeof(void);
        }

        public override Type VisitAssign(BRAQParser.AssignContext context)
        {
            Type right_type = context.assignee.Accept(this);
            if (context.id_name != null)
            {
                if (var_to_local_def.ContainsKey(context.id_name))
                {
                    Type target_type = locals_type[var_to_local_def[context.id_name]];
                    //check types
                    if (target_type != right_type)
                    {
                        Console.WriteLine(
                            $"type mismatch: assigned {right_type} to variable {context.id_name.Text} of type {target_type} [Line {context.id_name.Line}]");
                        throw new TypeMismatchError();
                    }
                    return type_dict[context] = typeof(void);
                }
                
                try
                {
                    Type target_type = current_method.arguments.First(x => x.a == context.id_name.Text).b;
                    if (target_type != right_type)
                    {
                        Console.WriteLine(
                            $"type mismatch: assigned {right_type} to variable {context.id_name.Text} of type {target_type} [Line {context.id_name.Line}]");
                        throw new TypeMismatchError();
                    }
                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine($"unknown variable {context.id_name.Text} [Line {context.id_name.Line}]");
                    throw new UnknownVariableException();
                }
                    
                    
                return type_dict[context] = typeof(void);
            }

            return type_dict[context] = right_type;


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
            
            if (TryResolveOwnShortMethod(context, out var ownMethodInfo))
            {
                result_box.token_to_user_function[context.calee] = ownMethodInfo;
                return type_dict[context] = ownMethodInfo.return_type;
                
            }
            
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
                
                result_box.token_to_outer_function[function_token] = predef_function_info;
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

        public override Type VisitReturn_stmt(BRAQParser.Return_stmtContext context)
        {
            Type expected_type = result_box.methodInfo.return_type;
            Type actual_type;
            if (context.return_value != null)
            {
                actual_type = context.return_value.Accept(this);
            }
            else
            {
                actual_type = typeof(void);
            }

            if (expected_type != actual_type)
            {
                Console.WriteLine($"return type mismatch: expected {expected_type} but got {actual_type}");
                throw new TypeMismatchError();
            }

            return type_dict[context] = expected_type;
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

            if (TryResolveOwnMethod(context, out var ownMethodInfo))
            {

                result_box.token_to_user_function[context.calee] = ownMethodInfo;
                return type_dict[context] = ownMethodInfo.return_type;
            }

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
                
                result_box.token_to_outer_function[function_token] = predef_function_info;
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
            if (var_to_local_def.ContainsKey(context.id_name))
            {
                var def_point = var_to_local_def[context.id_name];
                Type t = locals_type[def_point];
                return type_dict[context] = t;
            }
            
            try
            {
                return type_dict[context] = result_box.argtypes[context.id_name.Text];
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine($"unknown variable {context.id_name.Text} [Line {context.id_name.Line}]");
                throw new UnknownVariableException();
            }
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

        private static Type string_to_type(string name)
        {
            return Type.GetType($"System.{name}");
        }

        private bool TryResolveOwnMethod(BRAQParser.CallContext context, out OwnMethodInfo ownMethodInfo)
        {
            IToken name = context.calee;
            List<Type> args = context.expr().Select(x => type_dict[x]).ToList();
            try
            {
                var handle = user_functions
                    .First(x => x.name == name.Text 
                                && x.arguments.Count==args.Count
                                && x.arguments.Select(r => r.b).Zip(args, (r,w)=> new Pair<Type, Type>(r, w))
                                    .All(rec => rec.a==rec.b)
                        );
                ownMethodInfo = handle;
                
                return true;
            }
            catch (InvalidOperationException)
            {
                ownMethodInfo = null;
                return false;
            }
        }
        
        private bool TryResolveOwnShortMethod(BRAQParser.Short_callContext context, out OwnMethodInfo ownMethodInfo)
        {
            IToken name = context.calee;
            
            //найдём аргумент
            List<Type> args;
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

            args = new List<Type>(new[] {argument_type});

            try
            {
                var handle = user_functions
                    .First(x => x.name == name.Text 
                                && x.arguments.Count==args.Count
                                && x.arguments.Select(r => r.b).Zip(args, (r,w)=> new Pair<Type, Type>(r, w))
                                    .All(rec => rec.a==rec.b)
                    );
                ownMethodInfo = handle;
                
                return true;
            }
            catch (InvalidOperationException)
            {
                ownMethodInfo = null;
                return false;
            }
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