using System;
using System.Collections.Generic;
using System.Reflection;

namespace BRAQ
{
    public class Predefs
    {
        private static Dictionary<string, string> hack = new Dictionary<string, string>();

        static Predefs()
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
        
        
        
        
        public static double to_double(int x)
        {
            return x;
        }
        
        public static double sin(double x)
        {
            return Math.Sin(x);
        }
        
        public static double exp(double x)
        {
            return Math.Exp(x);
        }

        public static double sqr(double x) => Math.Pow(x, 2);
        public static int sqr(int x) => x * x;

        public static double pow(double x, double y) => Math.Pow(x, y);

    }
}