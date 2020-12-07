using System;
using System.Collections.Generic;
using System.Linq;

namespace BRAQ
{
    public class ImportVisitor : BRAQParserBaseVisitor<int>
    {
        List<Type> _imported_names = new List<Type>();

        public static List<Type> GetImports(BRAQParser.ProgramContext context)
        {
            var visitor = new ImportVisitor();
            context.Accept(visitor);
            return visitor._imported_names;
        }
        
        public override int VisitProgram(BRAQParser.ProgramContext context)
        {
            context.imports().ToList().ForEach(x => x.Accept(this));
            return 0;
        }

        public override int VisitImports(BRAQParser.ImportsContext context)
        {
            try
            {
                Type t = Type.GetType(context.containing.GetText());
                if(t==null) throw new BindError();
                this._imported_names.Add(t);
                return 0;
            }
            catch (BindError e)
            {
                Console.WriteLine($"unknown name {context.containing.GetText()} [Line {context.Stop.Line}]");
                throw e;
            }
        }
    }
}