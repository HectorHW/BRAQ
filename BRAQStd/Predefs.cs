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

    }
}