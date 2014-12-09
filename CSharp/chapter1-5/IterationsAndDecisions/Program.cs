using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IterationsAndDecisions
{
    class Program
    {
        static void Main(string[] args)
        {
            ForLoopExample();

            Console.ReadLine();
        }

        // Простой цикл for.
        static void ForLoopExample()
        {
            // Обратите внимание, что переменная i является видимой только
            // в контексте этого цикла for.
            //for(int i = 0; i < 4; i++)
            //{
            //    Console.WriteLine("Number is: {0} ", i);
            //}

            // Здесь переменная i уже не видима.

            // Проход по элементам массива с помощью foreach.
            //int[] iArray = { 10, 20, 30, 40 };
            //string[] carTypes = { "Ford", "BMW", "Yugo", "Honda" };
            //foreach (string ithem in carTypes)
            //    Console.WriteLine(ithem);
            //foreach (int ithem in iArray)
            //    Console.WriteLine(ithem);

            //int[] Numbers = { 10, 20, 30, 40, 1, 2, 3, 8 };
            //var Subset = from i in Numbers where i < 10 select i;
            //Console.Write("Values in Subset: ");
            //foreach (var item in Subset)
            //{
            //    Console.Write("{0} ", item);
            //}

            //string userIsDone = "";
            // Проверить строку в нижнем регистре
            //while (userIsDone.ToLower() != "yes")
            //{
            //    Console.WriteLine("In while loop");
            //    Console.Write("Are you done? [yes] [no]: ");
            //    userIsDone = Console.ReadLine();
            //}

            //do
            //{
            //    Console.WriteLine("In do/while loop");
            //    Console.Write("Are you done? [yes] [no]: ");
            //    userIsDone = Console.ReadLine();
            //} 
            //while (userIsDone.ToLower() != "yes"); // Обратите внимание на точку с запятой!

            Console.Write("Enter your favirite day of the week: ");
            DayOfWeek favDay;
            try
            {
                favDay = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Bad input!");
                return;
            }

            switch (favDay)
            {
                case DayOfWeek.Friday:
                    Console.WriteLine("Yes, Friday rules!");
                    break;
                case DayOfWeek.Monday:
                    Console.WriteLine("Another day, another dollar");
                    break;
                case DayOfWeek.Saturday:
                    Console.WriteLine("Great day indeed");
                    break;
                case DayOfWeek.Sunday:
                    Console.WriteLine("Football!");
                    break;
                case DayOfWeek.Thursday:
                    Console.WriteLine("Almost Friday...");
                    break;
                case DayOfWeek.Tuesday:
                    Console.WriteLine("At least it is not Monday");
                    break;
                case DayOfWeek.Wednesday:
                    Console.WriteLine("A fine day.");
                    break;
            }
            

            
        }
    }
}
