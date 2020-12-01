using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Antlr4.Runtime.Misc;
using BRAQ.Checkers;

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
            
            /*
            
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
            */

            var assigningResults = AssignCheckVisitor.getAssigners(ast);
            
            var typerResult = TyperVisitor.solveTypes(ast, assigningResults);

            //var type_dict = solving.a;
            //Dictionary<IToken, MethodInfo> function_table = solving.b;
            
            /*
            foreach (var keyValuePair in type_dict)
            {
                //if (keyValuePair.Key.GetType() == typeof(BRAQParser.Var_stmt_baseContext))
                
                    Console.WriteLine(keyValuePair.Value);
                
            }*/
            
            

            Console.WriteLine("solved types and functions");

            //checks
            
            CheckExecutor.runChecks(ast);
            
            //compiling
            
            AppDomain domain = AppDomain.CurrentDomain;
            AssemblyName asname = new AssemblyName();
            asname.Name = "BRAQAssembly";

            AssemblyBuilder asm = domain.DefineDynamicAssembly(asname, AssemblyBuilderAccess.RunAndSave);

            ModuleBuilder module = asm.DefineDynamicModule(
                prefixed_name, prefixed_name);
            
            TypeBuilder tpb = module.DefineType(
                "Program", TypeAttributes.Class);
            Dictionary<BRAQParser.Function_def_stmtContext, MethodBuilder> methods_to_generate = new Dictionary<BRAQParser.Function_def_stmtContext, MethodBuilder>();
            
            //create function handles
            foreach (var func in typerResult)
            {
                
                MethodBuilder mb = tpb.DefineMethod(func.Value.methodInfo.name, MethodAttributes.Public | MethodAttributes.Static,
                    CallingConventions.Standard,
                    func.Value.methodInfo.return_type, func.Value.methodInfo.arguments.Select(x => x.b).ToArray());

                methods_to_generate[func.Key] = mb;
                func.Value.methodInfo.method_builder = mb;
            }

            foreach (var m in typerResult.Keys)
            {
                var mb = typerResult[m].methodInfo.method_builder;
                ILGenerator method_il = mb.GetILGenerator();
                ILVisitor method_visitor = new ILVisitor(method_il, typerResult[m]);
                m.Accept(method_visitor);

            }
            
            Console.WriteLine("generated code");
            
            tpb.CreateType();
            try
            {
                var m = typerResult.First(x => x.Key.id_name.Text == "main");
                asm.SetEntryPoint(m.Value.methodInfo.method_builder);
            }catch(InvalidOperationException ignored){}
            
        
            asm.Save(prefixed_name);
        }
    }
}