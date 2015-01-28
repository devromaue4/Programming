using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericPoint
{
    public struct Point<T>
    {
        private T xPos;
        private T yPos;

        public Point(T x, T y)
        {
            xPos = x;
            yPos = y;
        }

        public T X 
        {
            get { return xPos; }
            set { xPos = value; }
        }

        public T Y
        {
            get { return yPos; }
            set { yPos = value; }
        }

        public override string ToString()
        {
            return string.Format("[{0}, {1}]", xPos, yPos);
        }

        // Сбросить поля в стандартные значения
        // для заданного параметра типа
        public void ResetPoint()
        {
            xPos = default(T);
            yPos = default(T);
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with Generic Structures *****\n");

            // Объект Point в котором используется int
            Point<int> p = new Point<int>(10, 10);
            Console.WriteLine("p.ToString()={0}", p.ToString());
            p.ResetPoint();
            Console.WriteLine("p.ToString()={0}", p.ToString());
            Console.WriteLine();

            // Объект Point в котором используется double
            Point<double> p2 = new Point<double>(5.4, 3.3);
            Console.WriteLine("p2.ToString()={0}", p2.ToString());
            p2.ResetPoint();
            Console.WriteLine("p2.ToString()={0}", p2.ToString());
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
