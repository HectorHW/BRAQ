using System;
using System.Linq;
using Antlr4.Runtime.Tree;

namespace BRAQ
{
    using BRAQ_grammar;
    public class BRAQEvaluatorVisitor : BRAQParserBaseVisitor<int>
    {
        public override int VisitProgram(BRAQParser.ProgramContext context)
        {
            context.children.ToList().ForEach(x => Console.WriteLine( x.Accept(this)));
            //context.children.ToList() reverse - remove <EOF> TODO
            return 0;
        }


        /*public override int VisitBlock(BRAQParser.BlockContext context)
        {
            context.children.ToList().ForEach(x => Console.WriteLine( x.Accept(this)));
            return 0;
            //return base.VisitBlock(context);
        }*/

        public override int VisitStmt(BRAQParser.StmtContext context)
        {
            return context.GetChild(0).Accept(this);
        }

        public override int VisitVar_stmt(BRAQParser.Var_stmtContext context)
        {
            
            return base.VisitVar_stmt(context);
        }

        public override int VisitExpr(BRAQParser.ExprContext context)
        {
            if (context.ChildCount == 1)
            {
                return context.GetChild(0).Accept(this);
            }
            
            var left = context.left.Accept(this);
            string op = context.op.Text;
            var right = context.right.Accept(this);
            switch (op)
            {
                case "**": return (int)Math.Pow(left, right);
                case "+": return left + right;
                case "-": return left - right;
                case "*": return left * right;
                case  "/": return left / right;
            }
            
            
            return base.VisitExpr(context);
        }

        public override int VisitGroup(BRAQParser.GroupContext context)
        {
            return context.GetChild(0).Accept(this);
        }

        public override int VisitLiteral(BRAQParser.LiteralContext context)
        {
            return int.Parse(context.GetText());
        }
    }
}