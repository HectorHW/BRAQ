namespace BRAQ.Checkers
{
    public class CheckExecutor
    {
        public static void runChecks(BRAQParser.ProgramContext context)
        {
            context.Accept(new BreakContinueChecker());
        }
    }
}