using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIInterfaceHierarchy
{
    interface IDrawable
    {
        void Draw();
    }

    interface IPrintable
    {
        void Print();
        void Draw();
    }

    interface IShape : IDrawable, IPrintable
    {
        int GetNumberOfSides();
    }

    class Rectangle : IShape
    {
        public int GetNumberOfSides() { return 4; }
        public void Print() { Console.WriteLine("Printing..."); }
        public void Draw() { Console.WriteLine("Drawing..."); }
    }

    class Square : IShape
    {
        public int GetNumberOfSides() { return 4; }
        public void Print() { Console.WriteLine("Printing..."); }
        public void IPrintable.Draw() { Console.WriteLine("Output to Printing..."); }
        public void IDrawable.Draw() { Console.WriteLine("Output to display..."); }
    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
