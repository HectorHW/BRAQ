using System;
using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime;

namespace BRAQ
{
    public class AssignCheckVisitor : BRAQParserBaseVisitor<int> //ищем присваивания 
    {

        public struct AssignCheckResult
        {
            public Dictionary<BRAQParser.Var_stmtContext, ParserRuleContext> def_to_assign;
            public Dictionary<IToken, BRAQParser.Var_stmtContext> token_to_def;
            public Dictionary<IToken, BRAQParser.Typed_idContext> token_to_arg;
        }
        
        private Dictionary<BRAQParser.Var_stmtContext, bool> assigned; //была ли присвоенна объявленная переменная
        private Dictionary<BRAQParser.Var_stmtContext, ParserRuleContext> assigners;
        // где была присвоена объявленная переменная
        
        private Dictionary<IToken, BRAQParser.Var_stmtContext> declaration;
        //где была объявлена упомянутая переменная

        private Dictionary<string, BRAQParser.Var_stmtContext> outer_scope;
        private Dictionary<string, BRAQParser.Var_stmtContext> local_scope;

        private Dictionary<string, BRAQParser.Typed_idContext> function_argument_scope;
        private Dictionary<IToken, BRAQParser.Typed_idContext> token_to_argument;

        protected List<Type> imported_types;
        
        bool inside_function = false;
        
        public AssignCheckVisitor()
        {
            assigned = new Dictionary<BRAQParser.Var_stmtContext, bool>();
            assigners = new Dictionary<BRAQParser.Var_stmtContext, ParserRuleContext>();
            declaration = new Dictionary<IToken, BRAQParser.Var_stmtContext>();

            outer_scope = new Dictionary<string, BRAQParser.Var_stmtContext>();
            local_scope = new Dictionary<string, BRAQParser.Var_stmtContext>();
            
            function_argument_scope = new Dictionary<string, BRAQParser.Typed_idContext>();
            token_to_argument = new Dictionary<IToken, BRAQParser.Typed_idContext>();
        }

        public override int VisitFunction_def_stmt(BRAQParser.Function_def_stmtContext context)
        {

            foreach (var typedId in context.typed_id())
            {
                function_argument_scope[typedId.id_name.Text] = typedId;
            }

            inside_function = true;

            context.function_body.Accept(this);
            
            if (assigned.ContainsValue(false))
            {
                string msg = $"variable {assigned.First(x => !x.Value).Key} was never assigned, cannot solve type.";
                Console.WriteLine(msg);
                throw new TypesolvingError();
            }
            

            inside_function = false;
            function_argument_scope.Clear();
            return 0;
        }

        public static Dictionary<BRAQParser.Function_def_stmtContext, AssignCheckResult> getAssigners(ParserRuleContext context, List<Type> imported_names)
        {
            var answ = new Dictionary<BRAQParser.Function_def_stmtContext, AssignCheckResult>();
            foreach (var fdef in ((BRAQParser.ProgramContext) context).function_def_stmt())
            {
                var v = new AssignCheckVisitor();
                v.imported_types = imported_names;
                fdef.Accept(v);
                
                if (v.assigned.ContainsValue(false))
                {
                    string msg = $"variable {v.assigned.First(x => !x.Value).Key} was never assigned, cannot solve type.";
                    Console.WriteLine(msg);
                    throw new TypesolvingError();
                }
                
                var res = new AssignCheckResult();
                res.def_to_assign = v.assigners;
                res.token_to_def = v.declaration;
                res.token_to_arg = v.token_to_argument;

                answ[fdef] = res;
            }
            return answ;

        }

        public override int VisitVar_stmt(BRAQParser.Var_stmtContext context)
        {
            if (local_scope.ContainsKey(context.id_name.Text))
            {
                Console.WriteLine($"redeclared local variable {context.id_name.Text} [Line {context.id_name.Line}]");
                throw new RedeclaredVariableException();
            }
            
            
            
            assigned[context] = false;
            local_scope[context.id_name.Text] = context;

            if (context.assignee != null)
            {
                context.assignee.Accept(this);
                assigned[context] = true;
                assigners[context] = context.assignee;
            }

            declaration[context.id_name] = context;
            return 0;
        }

        public override int VisitBlock(BRAQParser.BlockContext context)
        {
            var prev_outer = outer_scope;
            var prev_local = local_scope;

            outer_scope = outer_scope.Concat(local_scope)
                .GroupBy(x => x.Key)
                .ToDictionary(x => x.Key, x => x.Last().Value);
            // создаём словарь, в котором происходит объединение локальных и внешних переменных с возможным замещением
            
            local_scope = new Dictionary<string, BRAQParser.Var_stmtContext>();
            //для блока пока нет локальных переменных
            
            base.VisitBlock(context);
            //scope = outer_scope;
            local_scope = prev_local;
            outer_scope = prev_outer;
            return 0;
        }

        public override int VisitVar_node(BRAQParser.Var_nodeContext context)
        {
            if (TryResolve(context.id_name, out var var_definition_point))
            {
                if (!assigned[var_definition_point])
                {
                    string msg = $"using unassigned variable {context.id_name.Text}";
                    Console.WriteLine(msg);
                    throw new TypesolvingError();
                }

                declaration[context.id_name] = var_definition_point;
            }else if (TryResolveAsArg(context.id_name, out var argumentDefinition))
            {
                token_to_argument[context.id_name] = argumentDefinition;
            }else if (PredefsHelper.ResolveType(context.id_name.Text, imported_types) != null)
            {
            }
            else
            {
                Console.WriteLine($"unknown variable {context.id_name.Text} [Line {context.id_name.Line}]");
                throw new UnknownVariableException();
            }
            
            

            return 0;
        }

        public override int VisitAssign(BRAQParser.AssignContext context)
        {
            context.assignee.Accept(this);
            if (context.id_name == null) return 0;

            if (TryResolve(context.id_name, out var var_definition_point))
            {
                declaration[context.id_name] = var_definition_point;
                if (!assigned[var_definition_point])
                {
                    assigners[var_definition_point] = context.assignee;
                    assigned[var_definition_point] = true;
                }
                
            }else if (TryResolveAsArg(context.id_name, out var argument_definition))
            {
                token_to_argument[context.id_name] = argument_definition;
            }
            else
            {
                
                Console.WriteLine($"unknown variable {context.id_name.Text} [Line {context.id_name.Line}]");
                throw new UnknownVariableException();
            }
            
            
            return 0;
        }

        private bool TryResolve(IToken variable_token, out BRAQParser.Var_stmtContext definition_point)
        {
            if (local_scope.ContainsKey(variable_token.Text))
            {
                definition_point = local_scope[variable_token.Text];
                return true;
            }
            if (outer_scope.ContainsKey(variable_token.Text))
            {
                definition_point = outer_scope[variable_token.Text];
                return true;
            }
            /*
            Console.WriteLine($"unknown variable {variable_token.Text} [Line {variable_token.Line}]");
            throw new UnknownVariableException();*/
            definition_point = null;
            return false;
        }

        private bool TryResolveAsArg(IToken variable_token, out BRAQParser.Typed_idContext argument_definition)
        {
            if (inside_function && function_argument_scope.ContainsKey(variable_token.Text))
            {
                argument_definition = function_argument_scope[variable_token.Text];
                return true;
            }

            argument_definition = null;
            return false;
        }

    }

    public class UnknownVariableException : Exception
    {
    }

    class RedeclaredVariableException : Exception
    {
    }
}