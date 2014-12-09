using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomEnumeratorWithYeld
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with the Yield Keyword *****\n");
            Garage gar = new Garage();

            // Получить элементы используя GetEnumerator()
            foreach (Car item in gar)
            {
                Console.WriteLine("{0} is going {1} MPH", item.PetName, item.CurrentSpeed);
            }

            Console.WriteLine();

            // Получить элементы (в обратном порядке), используя именованный итератор
            foreach (Car item in gar.GetTheCars(true))
            {
                Console.WriteLine("{0} is going {1} MPH", item.PetName, item.CurrentSpeed);
            }

            Console.ReadLine();
        }
    }
}
