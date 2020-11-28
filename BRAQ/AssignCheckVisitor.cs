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
        }
        
        private Dictionary<BRAQParser.Var_stmtContext, bool> assigned; //была ли присвоенна объявленная переменная
        private Dictionary<BRAQParser.Var_stmtContext, ParserRuleContext> assigners;
        // где была присвоена объявленная переменная
        
        private Dictionary<IToken, BRAQParser.Var_stmtContext> declaration;
        //где была объявлена упомянутая переменная

        private Dictionary<string, BRAQParser.Var_stmtContext> outer_scope;
        private Dictionary<string, BRAQParser.Var_stmtContext> local_scope;

        
        
        public AssignCheckVisitor()
        {
            assigned = new Dictionary<BRAQParser.Var_stmtContext, bool>();
            assigners = new Dictionary<BRAQParser.Var_stmtContext, ParserRuleContext>();
            declaration = new Dictionary<IToken, BRAQParser.Var_stmtContext>();

            outer_scope = new Dictionary<string, BRAQParser.Var_stmtContext>();
            local_scope = new Dictionary<string, BRAQParser.Var_stmtContext>();
        }

        public static AssignCheckResult getAssigners(ParserRuleContext context)
        {
            var v = new AssignCheckVisitor();
            context.Accept(v);
            
            var res = new AssignCheckResult();
            res.def_to_assign = v.assigners;
            res.token_to_def = v.declaration;
            
            return res;

        }

        public override int VisitProgram(BRAQParser.ProgramContext context)
        {
            base.VisitProgram(context);
            if (assigned.ContainsValue(false))
            {
                string msg = $"variable {assigned.First(x => !x.Value).Key} was never assigned, cannot solve type.";
                Console.WriteLine(msg);
                throw new TypesolvingError();
            }
            return 0;
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

            outer_scope = outer_scope.Concat(local_scope)
                .GroupBy(x => x.Key)
                .ToDictionary(x => x.Key, x => x.Last().Value);
            // создаём словарь, в котором происходит объединение локальных и внешних переменных с возможным замещением
            
            local_scope = new Dictionary<string, BRAQParser.Var_stmtContext>();
            //для блока пока нет локальных переменных
            
            base.VisitBlock(context);
            //scope = outer_scope;
            local_scope = outer_scope;
            outer_scope = prev_outer;
            return 0;
        }

        public override int VisitVar_node(BRAQParser.Var_nodeContext context)
        {
            TryResolve(context.id_name, out var var_definition_point);
            
            if (!assigned[var_definition_point])
            {
                string msg = $"using unassigned variable {context.id_name.Text}";
                Console.WriteLine(msg);
                throw new TypesolvingError();
            }

            declaration[context.id_name] = var_definition_point;

            return 0;
        }

        public override int VisitAssign(BRAQParser.AssignContext context)
        {
            context.assignee.Accept(this);
            if (context.id_name == null) return 0;
            
            TryResolve(context.id_name, out var var_definition_point);
            declaration[context.id_name] = var_definition_point;
            if (!assigned[var_definition_point])
            {
                assigners[var_definition_point] = context.assignee;
                assigned[var_definition_point] = true;
            }
            
            return 0;
        }

        private void TryResolve(IToken variable_token, out BRAQParser.Var_stmtContext definition_point)
        {
            if (local_scope.ContainsKey(variable_token.Text))
            {
                definition_point = local_scope[variable_token.Text];
            }else if (outer_scope.ContainsKey(variable_token.Text))
            {
                definition_point = outer_scope[variable_token.Text];
            }
            else
            {
                Console.WriteLine($"unknown variable {variable_token.Text} [Line {variable_token.Line}]");
                throw new UnknownVariableException();
            }
        }

    }

    class UnknownVariableException : Exception
    {
    }

    class RedeclaredVariableException : Exception
    {
    }
}