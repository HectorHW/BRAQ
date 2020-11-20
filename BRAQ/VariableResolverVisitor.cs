using System;
using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;

namespace BRAQ
{
    public class VariableResolverVisitor : BRAQParserBaseVisitor<Pair<Dictionary<IToken, BRAQParser.Var_stmtContext>, ArrayList<BRAQParser.Var_stmtContext>>>
    {
        private ArrayList<BRAQParser.Var_stmtContext> variables
            = new ArrayList<BRAQParser.Var_stmtContext>();
        
        private Dictionary<IToken, BRAQParser.Var_stmtContext> dict 
            = new Dictionary<IToken, BRAQParser.Var_stmtContext>();

        //словарь переменных, доступных из внешних областей видимости с объявлениями
        private Dictionary<string, BRAQParser.Var_stmtContext> outer_scopes 
            = new Dictionary<string, BRAQParser.Var_stmtContext>();

        private Dictionary<string, BRAQParser.Var_stmtContext> local_scopes
            = new Dictionary<string, BRAQParser.Var_stmtContext>();

        public override Pair<Dictionary<IToken, BRAQParser.Var_stmtContext>, ArrayList<BRAQParser.Var_stmtContext>> 
            VisitProgram(BRAQParser.ProgramContext context)
        {
             base.VisitProgram(context);
             return new Pair<Dictionary<IToken, BRAQParser.Var_stmtContext>, ArrayList<BRAQParser.Var_stmtContext>>(dict, variables);
        }
        
        public override Pair<Dictionary<IToken, BRAQParser.Var_stmtContext>, ArrayList<BRAQParser.Var_stmtContext>> VisitVar_stmt(BRAQParser.Var_stmtContext context)
        {
            context.assignee?.Accept(this);

            string var_name = context.id_name.Text;
            
            if (local_scopes.ContainsKey(var_name)) throw new RedefinedVariableError(context.id_name);
            
            local_scopes[var_name] = context;
            
            variables.Add(context);
            
            if (context.assignee != null)
            {
                dict[context.id_name] = context;

            }
            
            return null;
        }

        public override Pair<Dictionary<IToken, BRAQParser.Var_stmtContext>, ArrayList<BRAQParser.Var_stmtContext>> VisitAssign(BRAQParser.AssignContext context)
        {
            if (context.id_name != null) //is an assignment
            {
                context.assignee.Accept(this);
            
                string var_name = context.id_name.Text;

                if (local_scopes.ContainsKey(var_name))
                {
                    dict[context.id_name] = local_scopes[context.id_name.Text];
                }else if (outer_scopes.ContainsKey(var_name))
                {
                    dict[context.id_name] = outer_scopes[context.id_name.Text];
                }else
                    throw new UndefinedVariableError(context.id_name);
                
                return null;
            }
            return base.VisitAssign(context);
        }

        public override Pair<Dictionary<IToken, BRAQParser.Var_stmtContext>, ArrayList<BRAQParser.Var_stmtContext>> 
            VisitVar_node(BRAQParser.Var_nodeContext context)
        {
            string var_name = context.id_name.Text;

            if (local_scopes.ContainsKey(var_name))
            {
                dict[context.id_name] = local_scopes[context.id_name.Text];
            }else if (outer_scopes.ContainsKey(var_name))
            {
                dict[context.id_name] = outer_scopes[context.id_name.Text];
            }else
                throw new UndefinedVariableError(context.id_name);

            return null;
        }

        private static Dictionary<IToken, BRAQParser.Var_stmtContext> overshadow(
            Dictionary<IToken, BRAQParser.Var_stmtContext> outer,
            Dictionary<IToken, BRAQParser.Var_stmtContext> inter)
        {
            var answ = new Dictionary<IToken, BRAQParser.Var_stmtContext>();
            foreach (var varStmtBaseContext in outer)
            {
                answ[varStmtBaseContext.Key] = varStmtBaseContext.Value;
            }
            
            foreach (var varStmtBaseContext in inter)
            {
                answ[varStmtBaseContext.Key] = varStmtBaseContext.Value;
            }

            return answ;
        }
    }

    class UndefinedVariableError : Exception
    {
        public IToken token;
        public UndefinedVariableError(IToken token)
        {
            this.token = token;
        }
    }

    class RedefinedVariableError : Exception
    {
        public IToken token;

        public RedefinedVariableError(IToken token)
        {
            this.token = token;
        }
    }
}