using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValueAndReferenceTypes
{
    struct Point
    {
        public int X;
        public int Y;

        public Point(int xPos, int yPos)
        {
            X = xPos;
            Y = yPos;
        }

        public void Display()
        {
            Console.WriteLine("X = {0}, Y = {1}", X, Y);
        }
    }

    class PointRef
    {
        public int X;
        public int Y;

        public PointRef(int xPos, int yPos)
        {
            X = xPos;
            Y = yPos;
        }

        public void Display()
        {
            Console.WriteLine("X = {0}, Y = {1}", X, Y);
        }
    }

    class ShareInfo
    {
        public string infoString;
        public ShareInfo(string info)
        {
            infoString = info;
        }
    }

    struct Rectangle
    {
        // Стрктура Rectangle содержит член ссылочного типа
        public ShareInfo recInfo;
        public int recTop, recLeft, recBottom, recRight;
        public Rectangle(string info, int top, int left, int bottom, int right)
        {
            recInfo = new ShareInfo(info);
            recTop = top;
            recLeft = left;
            recBottom = bottom;
            recRight = right;
        }

        public void Display()
        {
            Console.WriteLine("String = {0}, Top = {1}, Bottom = {2}, Left = {3}, Right = {4}",
                recInfo.infoString, recTop, recBottom, recLeft, recRight);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //ValueTypeAssignment();
            ValueTypeContaitingRefType();

            Console.ReadLine();
        }

        // Присваивание двух внутренних типов значений в результате
        // дает две независимых переменных в стеке
        static void ValueTypeAssignment()
        {
            Console.WriteLine("Assigning value types\n");
            //Point p1 = new Point(10, 24);
            //Point p2 = p1;

            //p1.Display();
            //p2.Display();

            //p1.X = 105;
            //Console.WriteLine("\n=> Changed p1.X\n");
            //p1.Display();
            //p2.Display();

            PointRef p1 = new PointRef(10, 24);
            PointRef p2 = p1;

            p1.Display();
            p2.Display();

            p1.X = 105;
            Console.WriteLine("\n=> Changed p1.X\n");
            p1.Display();
            p2.Display();
        }

        static void ValueTypeContaitingRefType()
        {
            // Создать первую переменную Rectangle
            Console.WriteLine("-> Creating r1");
            Rectangle r1 = new Rectangle("First Rect", 10, 10, 50, 50);

            // Присвоить новой переменной Rectangle переменную r1
            Console.WriteLine("-> Assigning r2 to r1");
            Rectangle r2 = r1;

            // Изменить некоторые значения в r2
            Console.WriteLine("-> Changing values of r2");
            r2.recInfo.infoString = "This is new info!";
            r2.recBottom = 4444;

            // Вывести значения из обеих перемнных Rectangle
            r1.Display();
            r2.Display();
        }
    }
}
