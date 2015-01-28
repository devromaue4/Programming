using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDelegate
{
    // Этот делегать может указывать на любой метод,
    // принимающий два целых и возвращающий целое
    public delegate int BinaryOp(int x, int y);

    // Этот класс содержит методы, на которые будет указывать BinaryOp
    class SimpleMath
    {
        public  int Add(int x, int y)
        { return x + y; }
        public  int Subtract(int x, int y)
        { return x - y; }
        public static int SquareNumber(int a)
        { return a * a; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Simple Delegate Example *****\n");

            // Создать объект делегата BinaryOp указывающий на SimpleMath.Add()
            SimpleMath m = new SimpleMath();
            BinaryOp b = new BinaryOp(m.Add);
            DisplayDelegateInfo(b);

            // Вызвать метод Add() непрямо с использованием объекта делегата
            Console.WriteLine("10 + 10 is {0}", b(10, 10));
            Console.ReadLine();
        }

        static void DisplayDelegateInfo(Delegate delObj)
        {
            //
            foreach (Delegate item in delObj.GetInvocationList())
            {
                Console.WriteLine("Method Name: {0}", item.Method);
                Console.WriteLine("Type Name: {0}", item.Target);
            }
        }
    }
}
