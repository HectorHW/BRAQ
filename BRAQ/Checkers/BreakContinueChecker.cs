using System;

namespace BRAQ.Checkers
{
    public class BreakContinueChecker : BRAQParserBaseVisitor<int>
    {
        private bool inside_loop = false;
        public override int VisitWhile_loop_stmt(BRAQParser.While_loop_stmtContext context)
        {
            context.cond?.Accept(this);
            bool prev_inside = inside_loop;
            inside_loop = true;
            context.body.Accept(this);
            inside_loop = prev_inside;
            
            return 0;
        }

        public override int VisitBreak_stmt(BRAQParser.Break_stmtContext context)
        {
            if (!inside_loop)
            {
                Console.WriteLine($"break outside of loop body [Line {context.Start.Line}]");
                throw new LoopControlError();
            }
            return 0;
        }

        public override int VisitContinue_stmt(BRAQParser.Continue_stmtContext context)
        {
            if (!inside_loop)
            {
                Console.WriteLine($"continue outside of loop body [Line {context.Start.Line}]");
                throw new LoopControlError();
            }
            return 0;
        }
    }

    public class LoopControlError : Exception
    {
    }
}