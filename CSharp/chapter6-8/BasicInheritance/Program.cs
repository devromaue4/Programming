using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicInheritance
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Basic Inheritance *****\n");

            //Car myCar = new Car(80);
            //myCar.Speed = 50;
            //Console.WriteLine("My car is going {0} MPH", myCar.Speed);

            MiniVan myMV = new MiniVan();
            myMV.Speed = 10;
            Console.WriteLine("My van is going {0} MPH", myMV.Speed);

            Console.ReadLine();

        }
    }
}
