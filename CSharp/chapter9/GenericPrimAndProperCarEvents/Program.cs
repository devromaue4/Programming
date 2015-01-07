using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericPrimAndProperCarEvents
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with Events *****\n");

            Car car = new Car("SlugBug", 100, 10);

            // Register event handlers.
            car.AboutToBlow += CarAboutToBlow;
            //car.Exploded += CarExploded;

            EventHandler<CarEventArgs> d = new EventHandler<CarEventArgs>(CarExploded);
            car.Exploded += d;

            Console.WriteLine("***** Speeding up *****");
            for (int i = 0; i < 6; i++)
                car.Accelerate(20);

            car.Exploded -= CarExploded;

            Console.WriteLine("\n***** Speeding up *****");
            for (int i = 0; i < 6; i++)
                car.Accelerate(20);

            Console.ReadLine();
        }

        //public static CarIsAlmostDoomed(object sender, CarEventArgs e)
        //{
        //    Console.WriteLine("=> Critical Mesage from Car: {0}", e.msg);
        //}

        public static void CarAboutToBlow(object sender, CarEventArgs e)
        {
            //Console.WriteLine("{0} says: {1}", sender, e.msg);

            if(sender is Car)
            {
                Car c = (Car)sender;
                Console.WriteLine("Critical Message from {0}: {1}", c.PetName, e.msg);
            }
        }

        public static void CarExploded(object sender, CarEventArgs e)
        {
            Console.WriteLine(e.msg);
        }
    }
}
