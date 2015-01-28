using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomConversion
{
    class Program
    {
        static void Main(string[] args)
        {
            Rectangle rec = new Rectangle(15, 4);
            Console.WriteLine("Rectangle : {0}", rec);
            rec.Draw();

            Square sq = new Square(5);
            Console.WriteLine("Square : {0}", sq);
            sq.Draw();

            sq = (Square)rec;
            Console.WriteLine("Square = (Square)Rectangle : {0}", sq);
            sq.Draw();

            Square sq2 = (Square)90;
            Console.WriteLine("sq2 = {0}", sq2);

            int side = (int)sq2;
            Console.WriteLine("Side length of sq2 = {0}", side);

            Square sq3 = new Square();
            sq3.Length = 83;
            
            // Попытка выполнить неявное приведение?
            Rectangle rec2 = sq3;
            Console.WriteLine("rec2 = {0}", rec2);

            Console.ReadLine();
        }
    }
}
