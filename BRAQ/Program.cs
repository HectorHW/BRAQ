using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Antlr4.Runtime.Misc;

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

            Dictionary<IToken, BRAQParser.Var_stmtContext> mention_to_def = dict_arr_pair.a;
            ArrayList<BRAQParser.Var_stmtContext> var_list = dict_arr_pair.b;

            Console.WriteLine(var_list.Count);
            Console.WriteLine(mention_to_def.Count);

            foreach (var keyValuePair in mention_to_def)
            {
                var id = keyValuePair.Value.ToStringTree();
                IToken t = keyValuePair.Key;

                Console.WriteLine("{0} {1} {2} : {3}", t.Text, t.Line, t.Column, id);
            }

            var solving = TyperVisitor.solveTypes(ast);

            var type_dict = solving.a;
            Dictionary<IToken, MethodInfo> function_table = solving.b;
            
            /*
            foreach (var keyValuePair in type_dict)
            {
                //if (keyValuePair.Key.GetType() == typeof(BRAQParser.Var_stmt_baseContext))
                
                    Console.WriteLine(keyValuePair.Value);
                
            }*/
            
            

            Console.WriteLine("solved types and functions");

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

            ILVisitor visitor = new ILVisitor(il, mention_to_def, var_list, type_dict, function_table);

            ast.Accept(visitor);

            Console.WriteLine("generated code");
            
            tpb.CreateType();


            asm.SetEntryPoint(main);
        
            asm.Save(prefixed_name);

            Console.WriteLine(Predefs.exp(1));
        }
    }
}