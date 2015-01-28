using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarEvents
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with Events *****\n");
            Car car1 = new Car("SlugBug", 100, 10);
            car1.AboutToBlow += CarIsAlmostDoomed;
            car1.AboutToBlow += CarIsAboutToBlow;
            car1.Exploded += CarExploded;

            Console.WriteLine("***** Speed up *****");
            for (int i = 0; i < 6; i++)
                car1.Accelerate(20);

            car1.Exploded -= CarExploded;

            Console.WriteLine("\n***** Speed up *****");
            for (int i = 0; i < 6; i++)
                car1.Accelerate(20);

            Console.ReadLine();

        }


        public static void CarIsAlmostDoomed(string msg)
        {
            Console.WriteLine(msg);
        }

        public static void CarIsAboutToBlow(string msg)
        {
            Console.WriteLine("=> Critical Message from Car: {0}", msg);
        }

        public static void CarExploded(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
