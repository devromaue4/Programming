using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceNameClash
{
    public interface IDrawToForm
    {
        void Draw();
    }

    public interface IDrawToMemory
    {
        void Draw();
    }

    public interface IDrawToPrinter
    {
        void Draw();
    }


    class Octagon : IDrawToForm, IDrawToMemory, IDrawToPrinter
    {
        void IDrawToForm.Draw()
        {
            Console.WriteLine("Drawing to form...");
        }

        void IDrawToMemory.Draw()
        {
            Console.WriteLine("Drawing to memory...");
        }

        void IDrawToPrinter.Draw()
        {
            Console.WriteLine("Drawing to printer...");
        }

        //public void Draw()
        //{
        //    Console.WriteLine("Drawing the Octagon...");
        //}
    }


    class Program
    {
        static void Main(string[] args)
        {
            Octagon oct = new Octagon();

            IDrawToForm dtf = (IDrawToForm)oct;
            dtf.Draw();
            IDrawToMemory dtm = (IDrawToMemory)oct;
            dtm.Draw();
            IDrawToPrinter dtp = (IDrawToPrinter)oct;
            dtp.Draw();

            Console.ReadLine();
        }
    }
}
