using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLambdaExpressions
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with Lambdas *****\n");
            TraditionalDelegateSyntax();
            AnonymousMethosSyntax();
            Console.WriteLine();
            LambdaExpressionSyntax();
            Console.ReadLine();
        }

        static void TraditionalDelegateSyntax()
        {
            // Создать список целых чисел
            List<int> list = new List<int>();
            list.AddRange(new int[] { 20, 1, 4, 8, 9, 44 });

            // Вызов FindAll() с использованием традиционного синтаксиса делегатов
            Predicate<int> callback = new Predicate<int>(IsEventNumber);
            List<int> eventNumbers = list.FindAll(callback);
            
            Console.WriteLine("Here are your even number:");
            foreach (int item in eventNumbers)
                Console.Write("{0}\t", item);
            Console.WriteLine();       
        }

        static void AnonymousMethosSyntax()
        {
            // Создать список целых чисел
            List<int> list = new List<int>();
            list.AddRange(new int[] { 20, 1, 4, 8, 9, 44 });

            // Теперь использовать анонимный метод
            List<int> eventNumbers = list.FindAll(delegate(int i) { return (i % 2) == 0; });

            Console.WriteLine("Here are your even number:");
            foreach (int item in eventNumbers)
                Console.Write("{0}\t", item);
            Console.WriteLine();
        }

        static void LambdaExpressionSyntax()
        {
            // Создать список целых чисел
            List<int> list = new List<int>();
            list.AddRange(new int[] { 20, 1, 4, 8, 9, 44 });

            // Теперь использовать лямбда-выражения C#
            List<int> eventNumbers = list.FindAll((i) =>
                {
                    Console.WriteLine("value of i is currently: {0}", i);
                    bool isEven =  ((i % 2) == 0);
                    return isEven;
                });

            Console.WriteLine("Here are your even number:");
            foreach (int item in eventNumbers)
                Console.Write("{0}\t", item);
            Console.WriteLine();
        }

        // Цель для делегата
        static bool IsEventNumber(int i)
        {
            // Это четное число
            return (i % 2) == 0;
        }
    }
}
