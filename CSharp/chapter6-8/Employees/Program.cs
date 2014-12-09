using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** The Employee Class Hierarchy *****\n");
            SalesPerson fred = new SalesPerson("Fred", 43, 93, 3000, "932-32-3232", 31);
            fred.GiveBonus(200);
            fred.DisplayStats();
            Console.WriteLine();

            Manager jon = new Manager("Jon", 50, 92, 100000, "333-23-2322", 9000);
            jon.GiveBonus(300);
            jon.DisplayStats();

            Employee em = new SalesPerson();

            Console.ReadLine();
        }
    }
}
