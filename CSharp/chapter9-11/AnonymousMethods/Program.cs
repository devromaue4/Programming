using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnonymousMethods
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Anonymous Methods *****\n");

            int aboutToBlowCounter = 0;

            Car car1 = new Car("SlugBug", 100, 10);

            // Зарегестрировать обработчики событий в виде анонимных методов
            car1.AboutToBlow += delegate {
                aboutToBlowCounter++;
                Console.WriteLine("Eek! Going too fast!");
            };
            car1.AboutToBlow += delegate(object sender, CarEventArgs e) {
                aboutToBlowCounter++; 
                Console.WriteLine("Message from Car: {0}", e.msg);
            };
            car1.Exploded    += delegate(object sender, CarEventArgs e) {
                Console.WriteLine("Fatal Message from Car: {0}", e.msg);
            };

            // Это в конечном итоге инициирует события
            for (int i = 0; i < 6; i++)
                car1.Accelerate(20);

            Console.WriteLine("AboutToBlow event was fired {0} times.", aboutToBlowCounter);

            Console.ReadLine();
        }
    }
}
