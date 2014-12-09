using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithGenericCollections
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with Generic Collections *****\n");

            UseGenericList();
            Console.WriteLine();

            UseGenericStack();
            Console.WriteLine();

            UseGenericQueue();
            Console.WriteLine();

            UseSortedSet();

            Console.ReadLine();
        }

        static void UseGenericList()
        {
            // Создать список объектов Person и заполнить его с помошью
            // синтаксиса инициализации объектов/коллекций
            List<Person> people = new List<Person>()
            {
                new Person { FirstName = "Homer", LastName = "Simpson", Age = 47 },
                new Person { FirstName = "Marge", LastName = "Simpson", Age = 45 },
                new Person { FirstName = "Lisa", LastName = "Simpson", Age = 9 },
                new Person { FirstName = "Bart", LastName = "Simpson", Age = 8 }
            };

            // Вывести на консоль количество элементов в списке
            Console.WriteLine("Items in list: {0}", people.Count);

            // Выполнить перчисление по списку
            foreach (Person item in people)
                Console.WriteLine(item);

            // Вставить новую персону
            Console.WriteLine("\n->Inserting new Person.");
            people.Insert(2, new Person { FirstName = "Maggi", LastName = "Simpson", Age = 2 });
            Console.WriteLine("Items in list: {0}", people.Count);

            // Скопировать данные в новый массив
            Person[] arrOfPeople = people.ToArray();
            for (int i = 0; i < arrOfPeople.Length; i++)
            {
                Console.WriteLine("First Names: {0}", arrOfPeople[i].FirstName);
            }
        }

        static void UseGenericStack()
        {
            Stack<Person> stackOfPeople = new Stack<Person>();
            stackOfPeople.Push(new Person { FirstName = "Homer", LastName = "Simpson", Age = 47 });
            stackOfPeople.Push(new Person { FirstName = "Magre", LastName = "Simpson", Age = 45 });
            stackOfPeople.Push(new Person { FirstName = "Lisa", LastName = "Simpson", Age = 9 });

            // Просмотреть верхний элемент, вытолкнуть его и просмотреть снова
            Console.WriteLine("First person is: {0}", stackOfPeople.Peek());
            Console.WriteLine("Popped off {0}", stackOfPeople.Pop());
            Console.WriteLine("\nFirst person is: {0}", stackOfPeople.Peek());
            Console.WriteLine("Popped off {0}", stackOfPeople.Pop());
            Console.WriteLine("\nFirst person is: {0}", stackOfPeople.Peek());
            Console.WriteLine("Popped off {0}", stackOfPeople.Pop());

            try
            {
                Console.WriteLine("\nFirst person is: {0}", stackOfPeople.Peek());
                Console.WriteLine("Popped off {0}", stackOfPeople.Pop());
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("\nError! {0}", ex.Message); // Ошибка! Стек пуст.
            }
        }

        static void GetCofee(Person p)
        {
            Console.WriteLine("{0} got coffee!", p.FirstName);
        }

        static void UseGenericQueue()
        {
            // Создать очередь из трех человек
            Queue<Person> peopleQ = new Queue<Person>();
            peopleQ.Enqueue(new Person { FirstName = "Homer", LastName = "Simpson", Age = 47 });
            peopleQ.Enqueue(new Person { FirstName = "Marge", LastName = "Simpson", Age = 45 });
            peopleQ.Enqueue(new Person { FirstName = "Lisa", LastName = "Simpson", Age = 9 });

            // Кто первый в очереди
            Console.WriteLine("{0} is first in line!", peopleQ.Peek().FirstName);

            // Удалить всех из очереди
            GetCofee(peopleQ.Dequeue());
            GetCofee(peopleQ.Dequeue());
            GetCofee(peopleQ.Dequeue());

            // Попробовать извлечь кого-то из очереди
            try
            {
                GetCofee(peopleQ.Dequeue());
            }
            catch(InvalidOperationException ex)
            {
                Console.WriteLine("Error! {0}", ex.Message); // Ошибка! Очередь пуста
            } 
        }

        static void UseSortedSet()
        {
            SortedSet<Person> setOfPeople = new SortedSet<Person>(new SortPeopleByAge())
            {
                new Person{ FirstName = "Homer", LastName = "Simpson", Age = 47 },
                new Person{ FirstName = "Marge", LastName = "Simpson", Age = 45 },
                new Person{ FirstName = "Lisa", LastName = "Simpson", Age = 9 },
                new Person{ FirstName = "Bart", LastName = "Simpson", Age = 8 }
            };

            foreach (Person item in setOfPeople)
                Console.WriteLine(item);
         
            Console.WriteLine();
            
            // Добавить еще людей разного возраста
            setOfPeople.Add(new Person { FirstName = "Saku", LastName = "Jones", Age = 1 });
            setOfPeople.Add(new Person { FirstName = "Mikko", LastName = "Jones", Age = 32 });

            // Элементы по-прежнему отсортированы по возрасту
            foreach(Person p in setOfPeople)
                Console.WriteLine(p);
        }

    } // end class Program
}// end namespace FunWithGenericCollections
