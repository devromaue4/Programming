using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicConsoleIO
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Basic Console I/O *****");
            //GetUserData();
            FormatNumericalData();
            Console.ReadLine();
        }

        static void GetUserData()
        {
            // Получить информацыю об имени и возрасте.
            Console.WriteLine("Please enter your name: ");
            string userName = Console.ReadLine();
            Console.WriteLine("Please enter your age: ");
            string userAge = Console.ReadLine();

            // Изменить цвет переднего плана просто ради интереса
            ConsoleColor prevColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;

            // Вывести полученные сведения на консоль
            Console.WriteLine("Hello {0}! You are {1} years old.", userName, userAge);

            // Востановить предыдущий цвет переднего плана
            Console.ForegroundColor = prevColor;
        }

        static void FormatNumericalData()
        {
            Console.WriteLine("The value 99999 in various formats:");
            Console.WriteLine("c format: {0:c}", 99999);
            Console.WriteLine("d9 format: {0:d9}", 99999);
            Console.WriteLine("f3 format: {0:f3}", 99999);
            Console.WriteLine("n format: {0:n}", 99999);

            // Обратите внимание, что использование верхнего и нижнего регистра для х
            // определяет, в каком регистре отображаются символы в шеснадцатеричном формате.
            Console.WriteLine("E format: {0:E}", 99999);
            Console.WriteLine("e format: {0:e}", 99999);
            Console.WriteLine("X format: {0:X}", 99999);
            Console.WriteLine("x format: {0:x}", 99999);
        }
    }
}
