using System;
using System.Collections.Generic;
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
        }
        
        
        
        
        public static MethodInfo Resolve(string method_name, Type[] arguments)
        {
            if (hack.ContainsKey(method_name))
            {
                method_name = hack[method_name];
            }

            var initial_attempt =  typeof(Predefs).GetMethod(method_name, arguments);
            if (initial_attempt != null) return initial_attempt;

            return null;
            
        }
    }
}