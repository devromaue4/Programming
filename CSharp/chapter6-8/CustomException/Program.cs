using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomException
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with Custom Exceptions *****\n");
            Car car = new Car("Rusty", 90);
            try
            {
                car.Accelerate(50);
            }
            catch(CarIsDeadException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.ErrorTimeStamp);
                Console.WriteLine(ex.CauseOfError);
            }

            Console.ReadLine();
        }
    }
}
