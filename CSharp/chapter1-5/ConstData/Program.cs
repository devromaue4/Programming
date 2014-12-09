using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstData
{
    class MyMathClass
    {
        //public const double PI = 3.14;
        public static readonly double PI;// = 3.14;

        static MyMathClass() { PI = 3.14; }
        public MyMathClass()
        {
            //PI = 3.14;
        }

        //public void ChangePI()
        //{
        //    // Errror
        //    PI = 1.14;
        //}
    }

    class Program
    {
        static void Main(string[] args)
        {
            MyMathClass mc = new MyMathClass();
            Console.WriteLine("***** Fun with const *****\n"); ;
            Console.WriteLine("The value of PI is: {0}", MyMathClass.PI);
            //Console.WriteLine("The value of PI is: {0}", mc.PI);

            // Error
            //MyMathClass.PI = 2.333;

            Console.ReadLine();
        }
    }
}
