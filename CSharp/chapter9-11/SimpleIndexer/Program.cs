using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleIndexer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with Indexers *****\n");

            PersonCollection PresonCollect = new PersonCollection();
            PresonCollect[0] = new Person("Homer", "Sinpson", 40);
            PresonCollect[1] = new Person("Marge", "Sinpson", 38);
            PresonCollect[2] = new Person("Lisa", "Sinpson", 9);
            PresonCollect[3] = new Person("Bart", "Sinpson", 7);
            PresonCollect[4] = new Person("Maggie", "Sinpson", 2);

            // Получить и отобразить элементы с использованием индексатора
            for (int i = 0; i < PresonCollect.Count; i++)
            {
                Console.WriteLine("Person number: {0}", i);
                Console.WriteLine("Name: {0} {1}", PresonCollect[i].FirstName, PresonCollect[i].LastName);
                Console.WriteLine("Age: {0}", PresonCollect[i].Age);
                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
}
