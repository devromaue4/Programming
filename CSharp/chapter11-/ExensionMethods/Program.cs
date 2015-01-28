using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyExtensionMethods;

namespace ExensionMethods
{
    class Program
    {
        static void Main(string[] args)
        {
            //
            // Использование расширяющих методов
            //

            Console.WriteLine("***** Fun wit Extension Methods *****\n");

            // В int появилась новая идентичность!
            int myInt = 12345678;
            myInt.DisplayDefiningAssembly();

            // То же и у DataSet!
            System.Data.DataSet d = new System.Data.DataSet();
            d.DisplayDefiningAssembly();

            // И у !
            System.Media.SoundPlayer sp = new System.Media.SoundPlayer();
            sp.DisplayDefiningAssembly();

            // Использовать новую функциональность int.
            Console.WriteLine("Value of myInt: {0}", myInt);
            Console.WriteLine("Reversed digits of myInt: {0}", myInt.ReverseDigits());

            Console.ReadLine();

        }
    }
}
