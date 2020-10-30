using System.Reflection.Emit;

namespace Calculator_Compiler
{
    public class ILVisitor : CParserBaseVisitor<int>
    {
        private readonly ILGenerator il;
        private LocalBuilder result;
        public ILVisitor(ILGenerator il)
        {
            this.il = il;
        }

        public override int VisitLiteral(CParser.LiteralContext context)
        {
            il.Emit(OpCodes.Ldc_I4, int.Parse(context.GetText()));
            return 0;
        }

        public override int VisitExpr(CParser.ExprContext context)
        {
            if (context.ChildCount == 1)
            {
                context.GetChild(0).Accept(this);
                return 0;
            }
            //two -> binary op

            context.left.Accept(this);
            context.right.Accept(this);

            switch (context.op.Text)
            {
                case "+":
                    il.Emit(OpCodes.Add);
                    break;
                case "-":
                    il.Emit(OpCodes.Sub);
                    break;
                case "*":
                    il.Emit(OpCodes.Mul);
                    break;
                case "/":
                    il.Emit(OpCodes.Div);
                    break;
                case "%":
                    il.Emit(OpCodes.Rem);
                    break;
            }

            return 0;
        }

        public override int VisitGroup(CParser.GroupContext context)
        {
            context.containing.Accept(this);
            return 0;
        }

        public override int VisitStmt(CParser.StmtContext context)
        {
            
            
            context.containing.Accept(this);

            il.Emit(OpCodes.Stloc, result); // var_i = stack[0]
            
            il.EmitWriteLine(result); // print(var_i)
            
            return 0;
        }

        public override int VisitProgram(CParser.ProgramContext context)
        {
            result = il.DeclareLocal(typeof(int));

            base.VisitProgram(context);
            
            il.Emit(OpCodes.Ret);

            return 0;
        }
    }
}