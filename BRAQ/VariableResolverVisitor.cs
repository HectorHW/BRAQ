using System;
using System.Collections.Generic;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;

namespace BRAQ
{
    public class VariableResolverVisitor : BRAQParserBaseVisitor<Pair<Dictionary<IToken, int>, ArrayList<string>>>
    {
        private ArrayList<string> variables = new ArrayList<string>();
        
        private Dictionary<IToken, int> dict = new Dictionary<IToken, int>();

        public override Pair<Dictionary<IToken, int>, ArrayList<string>> VisitProgram(BRAQParser.ProgramContext context)
        {
             base.VisitProgram(context);
             return new Pair<Dictionary<IToken, int>, ArrayList<string>>(dict, variables);
        }

        public override Pair<Dictionary<IToken, int>, ArrayList<string>> VisitVar_stmt_base(BRAQParser.Var_stmt_baseContext context)
        {
            if (context.assignee != null)
            {
                context.assignee.Accept(this);
            }


            string var_name = context.id_name.Text;
            if (variables.Contains(var_name)) throw new RedefinedVariableError(context.id_name);

            variables.Add(var_name);
            return null;
        }

        public override Pair<Dictionary<IToken, int>, ArrayList<string>> VisitVar_node(BRAQParser.Var_nodeContext context)
        {
            string var_name = context.id_name.Text;
            if (!variables.Contains(var_name)) throw new UndefinedVariableError(context.id_name);

            int var_idx = variables.IndexOf(var_name);

            dict.Add(context.id_name, var_idx);
            
            return null;
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