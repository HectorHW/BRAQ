using System;
using System.Linq;

namespace BRAQ
{
    using BRAQ_grammar;
    using Antlr4.Runtime;
    internal class Program
    {
        public static void Main(string[] args)
        {
            string input;
            while ((input = Console.ReadLine()) != "exit")
            {
                var lexer = new BRAQLexer(new AntlrInputStream(input));
                var tokenStream = new CommonTokenStream(lexer);
                Console.WriteLine(tokenStream);
                var parser = new BRAQParser(tokenStream);
            
                var ast = parser.program();
                ast.Accept(new BRAQEvaluatorVisitor());
                
            }
            

        }
    }
}