using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BRAQ
{
    public static class PredefsHelper
    {
        private static Dictionary<string, string> hack = new Dictionary<string, string>();

        static PredefsHelper()
        {
            //overrides for keywords reserved in c#
            hack["double"] = "to_double";
            hack["int"] = "to_int";
            hack["string"] = "to_string";
        }



        public static Type ResolveType(string type_name, List<Type> imported_names)
        {
            var initial_attempt = Type.GetType(type_name);
            if (initial_attempt != null) return initial_attempt;

            var system_attempt = Type.GetType($"System.{type_name}");
            if (system_attempt!=null) return system_attempt;
            try
            {
                var import_attempt = imported_names.First(x => x.Name == type_name);
                return import_attempt;

            }
            catch (InvalidOperationException e)
            {
                return null;
            }
        }
        
        public static MethodInfo Resolve(Type base_name, string method_name, Type[] arguments)
        {
            if (hack.ContainsKey(method_name))
            {
                method_name = hack[method_name];
            }

            var initial_attempt =  typeof(Predefs).GetMethod(method_name, arguments);
            if (initial_attempt != null) return initial_attempt;
            
            //not found in my predefs

            if (base_name != null)
            {
                var dotted_attempt = base_name.GetMethod(method_name, arguments);
                return dotted_attempt;
            }
            
            
            //try Console
            
            var console_attempt = typeof(Console).GetMethod(method_name, arguments);
            if(console_attempt!=null) return console_attempt;
            
            //not found in Console
            //try Math

            var math_attempt = typeof(Math).GetMethod(method_name, arguments);
            if (math_attempt != null) return math_attempt;

            return null;
            
        }
    }
}