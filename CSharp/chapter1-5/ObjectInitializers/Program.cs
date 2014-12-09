using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectInitializers
{
    class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point() {}
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void DisplaySats()
        {
            Console.WriteLine("[{0}, {1}]", X, Y);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with Object Init Syntax *****\n");

            // Создать объект Point с установкой каждого свойства в ручную
            Point FirstPoint = new Point();
            FirstPoint.X = 10;
            FirstPoint.Y = 10;
            FirstPoint.DisplaySats();

            // Создать объект Point с использованием специального конструктора
            Point SecondPoint = new Point(20, 20);
            SecondPoint.DisplaySats();

            // Создать объект Point с использованием синтаксиса инициализатора объекта
            Point FinalPoint = new Point { X = 30 , Y = 30 };
            FinalPoint.DisplaySats();

            Console.ReadLine();
        }
    }
}
