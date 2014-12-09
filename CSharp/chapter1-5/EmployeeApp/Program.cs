using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //
            Employee emp = new Employee("Marvin", 456, 30000);
            emp.GiveBonus(1000);
            emp.DisplayStats();

            // get/set
            emp.Name = "Marv";
           // emp.SetName("Marv");
            Console.WriteLine("Employee is name: {0}", emp.Name);

            Employee joe = new Employee();
            joe.Age++;

            //Employee emp2 = new Employee();
            //emp2.SetName("Xena the warrior princess");

            

            Console.ReadLine();
        }
    }
}
