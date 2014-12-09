using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    abstract class Shape
    {
        public string PetName { get; set; }

        public Shape(string name ="NoName"){PetName = name;}

        public abstract void Draw();
        //{
        //    Console.WriteLine("Inside Shape.Draw()");
        //}
    }

    class Circle : Shape
    {
        public Circle(){}
        public Circle(string name) : base(name){}

        public override void Draw()
        {
            Console.WriteLine("Drawing {0} the Circle", PetName);
        }
    }

    class Hexagon : Shape 
    {
        public Hexagon(){}
        public Hexagon(string name) : base(name) { }
        public override void Draw()
        {
            Console.WriteLine("Drawing {0} the Hexagon", PetName);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with Polymorphism *****");

            //Hexagon hex = new Hexagon("Beth");
            //hex.Draw();

            //Circle cr = new Circle("Cindy");
            //cr.Draw();

            Shape[] myShapes = { new Hexagon(), new Circle(), new Hexagon("Mick"),
                                   new Circle("Beth"), new Hexagon("Linda") };
            foreach (Shape item in myShapes)
                item.Draw();


            Console.ReadLine();
        }
    }
}
