using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomInterface
{
    class Program
    {
        static void DrawIn3D(IDraw3D i3d)
        {
            Console.WriteLine("-> Drawing IDraw3D compatible type");
            i3d.Draw3D();
        }

        static IPointy FindFirstPointyShape(Shape[] shapes)
        {
            foreach (Shape item in shapes)
            {
                if (item is IPointy)
                    return item as IPointy;
            }
            return null;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with Interfaces *****\n");
            Shape[] shapes = { new Hexagon(), new Circle(), new Triangle("Joe"), new Circle("Jojo") };
            //for (int i = 0; i < shapes.Length; i++)
            //{
            //    if (shapes[i] is IDraw3D)
            //        DrawIn3D((IDraw3D)shapes[i]);
            //}

            IPointy firstPointyItem = FindFirstPointyShape(shapes);
            Console.WriteLine("The item has {0} points", firstPointyItem.Points);

            Console.ReadLine();
        }
    }
}
