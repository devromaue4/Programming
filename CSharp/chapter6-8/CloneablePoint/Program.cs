using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloneablePoint
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with Object Cloning *****\n");
            Console.WriteLine("Cloned point1 and stored new Point in point2");
            Point point1 = new Point(100, 100, "Jane");
            Point point2 = (Point)point1.Clone();

            Console.WriteLine("Before modification:");
            Console.WriteLine("pont1: {0}", point1);
            Console.WriteLine("pont2: {0}", point2);

            point2.desc.PetName = "My new Point";
            point2.X = 9;

            Console.WriteLine("\nChanged point2.desc.PetName and point2.X");
            Console.WriteLine("After modification:");
            Console.WriteLine("pont1: {0}", point1);
            Console.WriteLine("pont2: {0}", point2);

            Console.ReadLine();
        }
    }
}
