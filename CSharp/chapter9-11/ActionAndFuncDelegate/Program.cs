using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionAndFuncDelegate
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with Action and Func *****\n");          

            // Использовать делегат Action<> для указания на DisplayMessage()
            //Action<string, ConsoleColor, int> actionTarget = new Action<string, ConsoleColor, int>(DisplayMessage);
            //actionTarget("Action Message!", ConsoleColor.Yellow, 5);

            Func<int, int, int> funcTarget = new Func<int, int, int>(Add);
            //int result = funcTarget(40, 40);
            int result = funcTarget.Invoke(40, 40);
            Console.WriteLine("40 + 40 = {0}", result);

            Func<int, int, string> funcTarget2 = new Func<int, int, string>(SumToString);
            string sum = funcTarget2(90, 300);
            Console.WriteLine("90 + 300 = {0}", sum);

            Console.ReadLine();
        }

        // Это цель для делегата
        static void DisplayMessage(string msg, ConsoleColor txColor, int printCount)
        {
            // Установить цвет текста консоли
            ConsoleColor prevClr = Console.ForegroundColor;
            Console.ForegroundColor = txColor;

            for (int i = 0; i < printCount; i++)
            {
                Console.WriteLine(msg);
            }

            // Востановить цвет
            Console.ForegroundColor = prevClr;
        }

        static int Add(int x, int y)
        {
            return x + y;
        }

        static string SumToString(int x , int y)
        {
            return (x + y).ToString();
        }
    }
}
