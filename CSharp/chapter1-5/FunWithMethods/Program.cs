using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithMethods
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with methods *****");

            //// Передать две переменных по значению
            //int x = 9, y = 10;
            //Console.WriteLine("Before call: X: {0}, Y: {1}", x, y);
            //Console.WriteLine("Answer is: {0}", Add(x, y));
            //Console.WriteLine("After call: X: {0}, Y: {1}", x, y);

            // Присваивать начальные значения локальным переменным,
            // используемым как выходные параметры, не обязательно,
            // при условии, что в таком качестве они используются первый раз.
            //int ans;
            //Add(90, 90, out ans);
            //Console.WriteLine("90 + 90 = {0}", ans);

            //int i; string str; bool b;
            //FillTheseValues(out i, out str, out b);
            //Console.WriteLine("Int is: {0}", i);
            //Console.WriteLine("String is: {0}", str);
            //Console.WriteLine("Boolean is: {0}", b);

            //string str1 = "Flip";
            //string str2 = "Flop";
            //Console.WriteLine("Before: {0}, {1}", str1, str2);
            //SwapStrings(ref str1, ref str2);
            //Console.WriteLine("After: {0}, {1}", str1, str2);

            // Передать разделяемый запятыми список значений double
            //double average;
            //average = CalculateAverage(4.0, 3.2, 5.7, 64.22, 87.2);
            //Console.WriteLine("Average of data is: {0}", average);

            //// ...или передать массив значений double
            //double[] data = { 4.0, 3.2, 5.7 };
            //average = CalculateAverage(data);
            //Console.WriteLine("Average of data is: {0}", average);

            //// Среднее из 0 равно 0
            //Console.WriteLine("Average of data is: {0}", CalculateAverage());

            //EnterLogData("Oh no! Grid can't find data");
            //EnterLogData("Oh no! I can't find the payroll data", "CFO");

            DisplayFancyMessage(message: "Wow! Very Fancy ideed!",
                txtColor: ConsoleColor.DarkRed,
                bgColor: ConsoleColor.White);
            DisplayFancyMessage(bgColor: ConsoleColor.Green,
                message: "Testing...",
                txtColor: ConsoleColor.DarkBlue);

            Console.ReadLine();
        }

        // По умолчанию аргументы передаются по значению.
        static int Add(int x, int y)
        {
            int ans = x + y;

            // Вызвающий код не увидит эти изменения,
            // т.к. изменяется копия исходных данных.
            x = 10000;
            y = 88888;
            return ans;
        }

        // Значения выходных параметров должны быть установлены вызываемым методом
        static void Add( int x, int y, out int ans )
        {
            ans = x + y;
        }

        // Множество выходных параметров
        static void FillTheseValues(out int a, out string b, out bool c)
        {
            a = 9;
            b = "Enjoy your string.";
            c = true;
        }

        // Ссылочные параметры
        public static void SwapStrings(ref string s1, ref string s2)
        {
            string tempStr = s1;
            s1 = s2;
            s2 = tempStr;
        }

        // Возврвщение среднего из некоторого количества значений double
        static double CalculateAverage(params double[] values)
        {
            Console.WriteLine("You sent me {0} doubles.", values.Length);
            double sum = 0;
            if (values.Length == 0)
                return sum;
            for (int i = 0; i < values.Length; i++)
                sum += values[i];
            return (sum / values.Length);
        }

        static void EnterLogData( string message, string owner = "Programmer")
        {
            Console.Beep();
            Console.WriteLine("Error: {0}", message);
            Console.WriteLine("Owner of Error: {0}", owner);
        }
        
        static void DisplayFancyMessage(ConsoleColor txtColor, ConsoleColor bgColor, string message)
        {
            // Сохранить старые цвета с целью их востановления после вывода сообщения.
            ConsoleColor oldTxtColor = Console.ForegroundColor;
            ConsoleColor oldBgColor = Console.BackgroundColor;

            // Установить новые цвета и вывести сообщение
            Console.ForegroundColor = txtColor;
            Console.BackgroundColor = bgColor;

            Console.WriteLine(message);

            // Востановить предыдущие цвета
            Console.ForegroundColor = oldTxtColor;
            Console.BackgroundColor = oldBgColor;
        }
    }
}
