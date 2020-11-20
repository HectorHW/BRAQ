using System;
using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;

namespace BRAQ
{
    class AssignCheckVisitor : BRAQParserBaseVisitor<int> //ищем присваивания 
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

        private Dictionary<string, BRAQParser.Var_stmtContext> scope;
        //какие объявления видны в данном участке
        
        
        public AssignCheckVisitor()
        {
            assigned = new Dictionary<BRAQParser.Var_stmtContext, bool>();
            assigners = new Dictionary<BRAQParser.Var_stmtContext, ParserRuleContext>();
            declaration = new Dictionary<IToken, BRAQParser.Var_stmtContext>();
            scope = new Dictionary<string, BRAQParser.Var_stmtContext>();
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
            assigned[context] = false;
            scope[context.id_name.Text] = context;

            if (context.assignee != null)
            {
                context.assignee.Accept(this);
                assigned[context] = true;
                assigners[context] = context.assignee;
            }
            return 0;
        }

        public override int VisitVar_node(BRAQParser.Var_nodeContext context)
        {
            if (!scope.ContainsKey(context.id_name.Text))
            {
                Console.WriteLine($"unknown variable {context.id_name.Text} [Line {context.id_name.Line}]");
                throw new UnknownVariableException();
            }

            var var_definition_point = scope[context.id_name.Text];
            
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
            if (context.id_name==null) return 0;
            
            if (!scope.ContainsKey(context.id_name.Text))
            {
                Console.WriteLine($"unknown variable {context.id_name.Text} [Line {context.id_name.Line}]");
                throw new UnknownVariableException();
            }
            var var_definition_point = scope[context.id_name.Text];

            if (!assigned[var_definition_point])
            {
                assigners[var_definition_point] = context.assignee;
                assigned[var_definition_point] = true;
            }
            
            return 0;
        }

    }

    class UnknownVariableException : Exception
    {
    }
}