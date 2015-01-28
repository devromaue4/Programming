﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericDelegate
{
    // Этот обобщенный делегат может вызывать любой метод, который
    // возвращает void и принимает параметр типа
    public delegate void MyGenericDelegate<T>(T arg);

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Generic Delegates *****\n");

            // Зарегестрировать цели
            MyGenericDelegate<string> strTarget = new MyGenericDelegate<string>(StringTarget);
            MyGenericDelegate<int> intTarget = new MyGenericDelegate<int>(IntTarget);
            
            strTarget("Some string data");
            intTarget(9);

            Console.ReadLine();
        }

        static void StringTarget(string arg)
        {
            Console.WriteLine("arg in uppercase is: {0}", arg.ToUpper());
        }

        static void IntTarget(int arg)
        {
            Console.WriteLine("++arg is: {0}", ++arg);
        }
    }
}
