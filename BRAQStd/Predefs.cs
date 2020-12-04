using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BRAQ
{
    public class Predefs
    {
        public static double to_double(int x)
        {
            return x;
        }

        public static double to_double(double x) => x;

        public static double to_double(string x) => Double.Parse(x);

        public static int to_int(double x) => (int)x;

        public static int to_int(int x) => x;

        public static int to_int(string x) => int.Parse(x);

        public static string to_string(int x) => x.ToString();
        
        public static string to_string(double x) => x.ToString();

        public static string to_string(string x) => x;
        
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
        public static int readInt() => int.Parse(Console.ReadLine());
        
        public static string readLine() => Console.ReadLine();
        
        public static double readDouble() => double.Parse(Console.ReadLine());

        public static void print(string line) => Console.WriteLine(line);

        public static void print(double d) => Console.WriteLine(d);

        public static void print(int i) => Console.WriteLine(i);
        public static void print(bool b) => Console.WriteLine(b);

        public static double constant(string name)
        {
            switch (name.ToLower())
            {
                case "pi": return Math.PI;
                case "e": return Math.E;
            }
            throw new BRAQRuntimeError($"unknown constant {name}");
        }
    }

    public class BRAQRuntimeError : Exception
    {
        public BRAQRuntimeError()
        {
        }

        public BRAQRuntimeError(string msg)
        {
            Console.WriteLine(msg);
            
        }
    }
}