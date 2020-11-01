using System;
using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;

namespace BRAQ
{
    public class VariableResolverVisitor : BRAQParserBaseVisitor<Pair<Dictionary<IToken, BRAQParser.Var_stmt_baseContext>, ArrayList<BRAQParser.Var_stmt_baseContext>>>
    {
        private ArrayList<BRAQParser.Var_stmt_baseContext> variables
            = new ArrayList<BRAQParser.Var_stmt_baseContext>();
        
        private Dictionary<IToken, BRAQParser.Var_stmt_baseContext> dict 
            = new Dictionary<IToken, BRAQParser.Var_stmt_baseContext>();

        //словарь переменных, доступных из внешних областей видимости с объявлениями
        private Dictionary<string, BRAQParser.Var_stmt_baseContext> outer_scopes 
            = new Dictionary<string, BRAQParser.Var_stmt_baseContext>();

        private Dictionary<string, BRAQParser.Var_stmt_baseContext> local_scopes
            = new Dictionary<string, BRAQParser.Var_stmt_baseContext>();

        public override Pair<Dictionary<IToken, BRAQParser.Var_stmt_baseContext>, ArrayList<BRAQParser.Var_stmt_baseContext>> 
            VisitProgram(BRAQParser.ProgramContext context)
        {
             base.VisitProgram(context);
             return new Pair<Dictionary<IToken, BRAQParser.Var_stmt_baseContext>, ArrayList<BRAQParser.Var_stmt_baseContext>>(dict, variables);
        }

        public override Pair<Dictionary<IToken, BRAQParser.Var_stmt_baseContext>, ArrayList<BRAQParser.Var_stmt_baseContext>> 
            VisitVar_stmt_base(BRAQParser.Var_stmt_baseContext context)
        {
            if (context.assignee != null)
            {
                context.assignee.Accept(this);
                
            }

            
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

        public override Pair<Dictionary<IToken, BRAQParser.Var_stmt_baseContext>, ArrayList<BRAQParser.Var_stmt_baseContext>> 
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


        public override Pair<Dictionary<IToken, BRAQParser.Var_stmt_baseContext>, ArrayList<BRAQParser.Var_stmt_baseContext>> VisitAssign_stmt_base(
            BRAQParser.Assign_stmt_baseContext context)
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
            
            
            return base.VisitAssign_stmt_base(context);
        }

        public override Pair<Dictionary<IToken, BRAQParser.Var_stmt_baseContext>, ArrayList<BRAQParser.Var_stmt_baseContext>> VisitRead_stmt_base(BRAQParser.Read_stmt_baseContext context)
        {
            
            string var_name = context.arg.id_name.Text;

            if (local_scopes.ContainsKey(var_name))
            {
                dict[context.arg.id_name] = local_scopes[ context.arg.id_name.Text];
            }else if (outer_scopes.ContainsKey(var_name))
            {
                dict[context.arg.id_name] = outer_scopes[ context.arg.id_name.Text];
            }else
                throw new UndefinedVariableError( context.arg.id_name);
            
            return base.VisitRead_stmt_base(context);
        }

        private static Dictionary<IToken, BRAQParser.Var_stmt_baseContext> overshadow(
            Dictionary<IToken, BRAQParser.Var_stmt_baseContext> outer,
            Dictionary<IToken, BRAQParser.Var_stmt_baseContext> inter)
        {
            var answ = new Dictionary<IToken, BRAQParser.Var_stmt_baseContext>();
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