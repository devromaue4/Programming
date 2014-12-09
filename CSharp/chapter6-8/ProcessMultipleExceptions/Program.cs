using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessMultipleExceptions
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Handling Multiple Exceptions *****\n");
            Car car = new Car("Rusty", 90);
            try
            {
                car.Accelerate(10);
            }
            catch (CarIsDeadException ex)
            {
                //Console.WriteLine(ex.Message);
                throw;
            }
            //catch(ArgumentOutOfRangeException ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
            //catch
            //{
            //    Console.WriteLine("Something bad happened...");
            //}

            Console.ReadLine();
        }
    }
}
