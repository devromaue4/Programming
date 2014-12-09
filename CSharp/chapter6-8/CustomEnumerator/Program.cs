using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CustomEnumerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Garage gar = new Garage();
            //IEnumerator it = gar.GetEnumerator();
           // it.MoveNext();
           // Car car = (Car)it.Current;

            //IEnumerator inum = ((IEnumerable)gar).GetEnumerator();
            //inum.MoveNext();
            //Car car = (Car)inum.Current;
            //Console.WriteLine(car.PetName);

            foreach (Car item in gar)
            {
                Console.WriteLine(item.PetName);
            }

            Console.ReadLine();
        }
    }
}
