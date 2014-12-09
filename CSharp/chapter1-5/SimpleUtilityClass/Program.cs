using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleUtilityClass
{
    // Статические классы могут содержать только статические члены!
    static class TimeUtilClass
    {
        public static void PrintTime()
        { Console.WriteLine(DateTime.Now.ToShortTimeString()); }
        public static void PrintDate()
        { Console.WriteLine(DateTime.Now.ToShortDateString()); }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with Static Classes *****\n");

            // Это работает нормально
            TimeUtilClass.PrintDate();
            TimeUtilClass.PrintTime();

            // Error comile!
            //TimeUtilClass ut = new TimeUtilClass();

            Console.ReadLine();
        }
    }
}
