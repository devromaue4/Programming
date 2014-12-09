using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectOverrides
{
    class Person
    {
        public string SSN { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public Person() { }
        public Person(string fName, string lName, int age)
        {
            FirstName = fName;
            LastName = lName; 
            Age = age;
        }

        public override string ToString()
        {
            string myState;
            myState = string.Format("[First Name: {0}; Last Name: {1}; Age: {2}]", FirstName, LastName, Age);
            return myState;
        }

        public override bool Equals(object obj)
        {
            //if (obj is Person && obj != null)
            //{
            //    Person temp;
            //    temp = (Person)obj;
            //    if (temp.FirstName == this.FirstName &&
            //        temp.LastName == this.LastName && temp.Age == this.Age)
            //        return true;
            //    else
            //        return false;

            //}

            //return false;

            return obj.ToString() == this.ToString();
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with System.Object *****\n");
            //Person p1 = new Person();

            // Использовать унаследованные члены System.Object
            //Console.WriteLine("ToString: {0}", p1.ToString());
            //Console.WriteLine("Hash code: {0}", p1.GetHashCode());
            //Console.WriteLine("Type: {0}", p1.GetType());

            //// Создать другую ссылку на p1
            //Person p2 = p1;
            //Object obj = p2;

            //// Указывают ли ссылки на один и тот же объект в памяти?
            //if(obj.Equals(p1) && p2.Equals(obj))
            //    Console.WriteLine("Same instance!");

            //
            Person p1 = new Person("Homer", "Simpson", 50);
            Person p2 = new Person("Homer", "Simpson", 50);
            Console.WriteLine("p1.ToString() = {0}", p1.ToString());
            Console.WriteLine("p2.ToString() = {0}", p2.ToString());

            //
            Console.WriteLine("p1 = p2?: {0}", p1.Equals(p2));

            //
            Console.WriteLine("Same hash codes?: {0}", p1.GetHashCode() == p2.GetHashCode());
            Console.WriteLine();

            //
            p2.Age = 45;
            Console.WriteLine("p1.ToString() = {0}", p1.ToString());
            Console.WriteLine("p2.ToString() = {0}", p2.ToString());
            Console.WriteLine("p1 = p2?: {0}", p1.Equals(p2));
            Console.WriteLine("Same hash codes?: {0}", p1.GetHashCode() == p2.GetHashCode());

            StaticMembersOfObject();

            Console.ReadLine();
        }

        static void StaticMembersOfObject()
        {
            Console.WriteLine();
            Person p3 = new Person("Sally", "Jones", 4);
            Person p4 = new Person("Sally", "Jones", 4);
            Console.WriteLine("P3 and P4 have same state: {0}", object.Equals(p3, p4));
            Console.WriteLine("P3 and P4 are point to same object: {0}", object.ReferenceEquals(p3, p4));
            Console.WriteLine();
        }
    }
}
