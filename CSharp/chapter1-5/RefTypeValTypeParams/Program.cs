using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefTypeValTypeParams
{
    class Person
    {
        public string personName;
        public int personAge;

        public Person() { }
        public Person(string name, int age)
        {
            personName = name;
            personAge = age;
        }

        public void DIsplay()
        {
            Console.WriteLine("Name: {0}, Age: {1}", personName, personAge);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Передача ссылочных типов по значению
            //Console.WriteLine("***** Passing Person object by value *****");
            //Person fred = new Person("Fred", 20);
            //Console.WriteLine("\nBefore by value call, Person is:");
            //fred.DIsplay();

            //SendAPersonByValue(fred);
            //Console.WriteLine("\nAfter by value call, Person is:");
            //fred.DIsplay();

            // Передача ссылочных типов по ссылке
            Console.WriteLine("***** Passing Person object by reference *****");
            Person Mel = new Person("Mel", 23);
            Console.WriteLine("\nBefore by value call, Person is:");
            Mel.DIsplay();

            SendAPersonByReference(ref Mel);
            Console.WriteLine("\nAfter by value call, Person is:");
            Mel.DIsplay();

            Console.ReadLine();
        }

        // Передача по значению (передается копия ссылки на объект)
        static void SendAPersonByValue(Person p)
        {
            p.personAge = 95;
            p.personName = "Roman";

            // Невозможно изменять то, на что ссылка указывает
            p = new Person("Nikki", 99);
        }

        // Передача по ссылке
        static void SendAPersonByReference(ref Person p)
        {
            p.personAge = 195;
            p.personName = "Sergey";

            // p теперь указывает на новый обьект в куче!
            p = new Person("Nikki", 999);
        }
    }
}
