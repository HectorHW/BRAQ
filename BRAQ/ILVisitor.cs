using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;

namespace BRAQ
{
    public class ILVisitor : BRAQParserBaseVisitor<int>
    {
        private ILGenerator il;
        private Dictionary<IToken, int> dict;
        private readonly ArrayList<string> _varList;
        
        private LocalBuilder for_print;

        public ILVisitor(ILGenerator il, Dictionary<IToken, int> dict, ArrayList<string> var_list)
        {
            this.il = il;
            this.dict = dict;
            _varList = var_list;
        }

        public override int VisitLiteral(BRAQParser.LiteralContext context)
        {
            if (context.num != null)
            {
                il.Emit(OpCodes.Ldc_I4, int.Parse(context.num.Text));
            }
            else
            {
                context.var_node_.Accept(this); //variable
            }
            return 0;
        }

        public override int VisitVar_node(BRAQParser.Var_nodeContext context)
        {
            dict.TryGetValue(context.id_name, out var var_id);
            il.Emit(OpCodes.Ldloc, var_id);
            return 0;
        }

        public override int VisitRead_stmt_base(BRAQParser.Read_stmt_baseContext context)
        {
            //call read
            il.EmitCall(OpCodes.Call, typeof(Console).GetMethod("ReadLine"), null);
            
            //call toInt
            il.EmitCall(OpCodes.Call, typeof(int).GetMethod("Parse", new[]{typeof(string)}), null);
            //save to var
            dict.TryGetValue(context.arg.id_name, out var var_id);
            il.Emit(OpCodes.Stloc, var_id);
            return 0;
        }

        public override int VisitExpr(BRAQParser.ExprContext context)
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

        public override int VisitGroup(BRAQParser.GroupContext context)
        {
            context.containing.Accept(this);
            return 0;
        }

        public override int VisitAssign_stmt_base(BRAQParser.Assign_stmt_baseContext context)
        {
            if (context.assignee != null)
            {
                context.assignee.Accept(this);
            }
            else
            {
                il.Emit(OpCodes.Ldc_I4_0);
            }

            int var_index = _varList.IndexOf(context.id_name.Text);

            il.Emit(OpCodes.Stloc, var_index);
            
            
            return 0;
        }

        public override int VisitPrint_stmt_base(BRAQParser.Print_stmt_baseContext context)
        {
            context.arg.Accept(this);

            il.Emit(OpCodes.Stloc, for_print); // var_i = stack[0]
            
            il.EmitWriteLine(for_print); // print(var_i)
            
            return 0;
        }

        public override int VisitVar_stmt_base(BRAQParser.Var_stmt_baseContext context)
        {
            if (context.assignee != null)
            {
                context.assignee.Accept(this);
            }
            else
            {
                il.Emit(OpCodes.Ldc_I4_0);
            }
            
            int var_index = _varList.IndexOf(context.id_name.Text);
            
            il.Emit(OpCodes.Stloc, var_index);
            
            return 0;
        }

        public override int VisitProgram(BRAQParser.ProgramContext context)
        {
            //define variables
            for (int i = 0; i < _varList.Count; i++)
            {
                il.DeclareLocal(typeof(int));
            }
            
            for_print = il.DeclareLocal(typeof(int));

            base.VisitProgram(context);

            il.Emit(OpCodes.Ret);
            
            return 0;
        }
    }
}