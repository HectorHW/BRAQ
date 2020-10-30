using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace BRAQ
{
    
    using Antlr4.Runtime;
    internal class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Expected single argument as a source file");
                return;
            }

            if (!File.Exists(args[0]))
            {
                Console.WriteLine("No file found");
                return;
            }

            string input = File.ReadAllText(args[0]);
            
            string prefixed_name = args[0].Split(".".ToCharArray())[0] + ".exe";
            
            var lexer = new BRAQLexer(new AntlrInputStream(input));
            var tokenStream = new CommonTokenStream(lexer);
            //Console.WriteLine(tokenStream);
            var parser = new BRAQParser(tokenStream);
        
            var ast = parser.program();
            
            var dict_arr_pair = ast.Accept(new VariableResolverVisitor());

            var idToNumDict = dict_arr_pair.a;
            var var_list = dict_arr_pair.b;
            
            

            foreach (var keyValuePair in idToNumDict)
            {
                int id = keyValuePair.Value;
                IToken t = keyValuePair.Key;

                Console.WriteLine("{0} {1} {2} : {3}", t.Text, t.Line, t.Column, id);
            }
                
                
                
                //compiling
                
                AppDomain domain = AppDomain.CurrentDomain;
                AssemblyName asname = new AssemblyName();
                asname.Name = "BRAQAssembly";

                AssemblyBuilder asm = domain.DefineDynamicAssembly(asname, AssemblyBuilderAccess.RunAndSave);

                ModuleBuilder module = asm.DefineDynamicModule(
                    prefixed_name, prefixed_name);
            
                TypeBuilder tpb = module.DefineType(
                    "Program", TypeAttributes.Class);
                // the method that will hold our expression code
                MethodBuilder main = tpb.DefineMethod(
                    "Main", MethodAttributes.Public | MethodAttributes.Static);

                ILGenerator il = main.GetILGenerator();

                ILVisitor visitor = new ILVisitor(il, idToNumDict, var_list);

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