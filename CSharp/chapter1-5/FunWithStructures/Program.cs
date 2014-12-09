using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithStructures
{
    class Program
    {
        struct Point
        {
            // Поля структуры
            public int X;
            public int Y;

            public Point(int xPos, int yPos)
            {
                X = xPos;
                Y = yPos;
            }

            // Инкрементировать x y
            public void Increment()
            {
                X++; Y++;
            }

            // Декрементировть x y
            public void Decrement()
            {
                X--; Y--;
            }

            // Отобразить текущую позицию
            public void Display()
            {
                Console.WriteLine("X = {0}, Y = {1}", X, Y);
            }
        }

        static void Main(string[] args)
        {
            // Создать начальный экземпляр Point
            Point myPoint = new Point(50, 60);
            //myPoint.X = 349;
           // myPoint.Y = 76;
            myPoint.Display();

            // Скоректировать значения X и Y
            myPoint.Increment();
            myPoint.Display();

            Console.ReadLine();
        }
    }
}
