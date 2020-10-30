using System;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using Antlr4.Runtime;

namespace Calculator_Compiler
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("provide one argument to compile");
                return;
            }
            

            string filename = args[0];
            string prefix = filename.Split(".".ToCharArray())[0];

            string prefixed_name = prefix + ".exe";

            if (!File.Exists(filename))
            {
                Console.WriteLine("No file found.");
                return;
            }
            
            string text = File.ReadAllText(filename);
            var lexer = new CLexer(new AntlrInputStream(text));
            var parser = new CParser(new BufferedTokenStream(lexer));
            var ast = parser.program();
            
            //compilation
            AppDomain domain = AppDomain.CurrentDomain;
            AssemblyName asname = new AssemblyName();
            asname.Name = "CalculatorExpression";

            AssemblyBuilder asm = domain.DefineDynamicAssembly(asname, AssemblyBuilderAccess.RunAndSave);

            ModuleBuilder module = asm.DefineDynamicModule(
                prefixed_name, prefixed_name);
            
            TypeBuilder tpb = module.DefineType(
                "Program", TypeAttributes.Class);
            // the method that will hold our expression code
            MethodBuilder main = tpb.DefineMethod(
                "Main", MethodAttributes.Public | MethodAttributes.Static);

            ILGenerator il = main.GetILGenerator();

            ILVisitor visitor = new ILVisitor(il);

            ast.Accept(visitor);

            tpb.CreateType();
            /*
            object[] noArgs = new object[0]; // массив аргументов

            asm.GetType("ExpressionExecutor")
                .GetMethod("WriteValue")
                .Invoke(null, noArgs);*/

            asm.SetEntryPoint(main);
            
            asm.Save(prefixed_name);

        }
    }
}