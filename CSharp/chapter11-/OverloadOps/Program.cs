using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverloadOps
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with overloaded Operators *****\n");

            Point ptOne = new Point(100, 100);
            Point ptTwo = new Point(40, 40);

            Console.WriteLine("ptOne = {0}", ptOne);
            Console.WriteLine("ptTwo = {0}", ptTwo);

            // Сложить две точки, чтобы получить большую?
            Console.WriteLine("ptOne + ptTwo: {0} ", ptOne + ptTwo);
            Console.WriteLine("ptOne += ptTwo: {0} ", ptOne += ptTwo);

            // Вычесть одну точку из другой, чтобы получить меньшую?
            Console.WriteLine("ptOne - ptTwo: {0}", ptOne - ptTwo);
            Console.WriteLine("ptOne -= ptTwo: {0} ", ptOne -= ptTwo);

            Point ptFive = new Point(1, 1);
            // Применение унарных операций ++ и -- к Point
            Console.WriteLine("++ptFive = {0}", ++ptFive);
            Console.WriteLine("--ptFive = {0}", --ptFive);

            Point ptSix = new Point(20, 20);
            // Применение тех же операций для постфиксного инкремента/декремента
            Console.WriteLine("ptSix++ = {0}", ptSix++);
            Console.WriteLine("ptSix-- = {0}", ptSix--);

            Console.WriteLine("ptOne == ptTwo : {0}", ptOne == ptTwo);
            Console.WriteLine("ptOne != ptTwo : {0}", ptOne != ptTwo);

            Console.WriteLine("ptOne < ptTwo : {0}", ptOne < ptTwo);
            Console.WriteLine("ptOne > ptTwo : {0}", ptOne > ptTwo);

            Console.WriteLine("ptOne <= ptTwo : {0}", ptOne <= ptTwo);
            Console.WriteLine("ptOne >= ptTwo : {0}", ptOne >= ptTwo);

            Console.ReadLine();
        }
    }
}
