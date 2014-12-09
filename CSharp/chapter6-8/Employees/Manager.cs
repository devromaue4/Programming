using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees
{
    // Менеджерам нужно занать количество их опционов на акции
    class Manager : Employee
    {
        public int StockOptions { get; set; }

        // Вернуть классу Manager стандартный конструктор
        public Manager() { }
        // В качестве общего правила, все подклассы должны явно вызывать
        // соответствующий конструктор базового класса
        public Manager(string fullName, int age, int empID, float currPay, string ssn, int numbOfOpts)
            : base(fullName, age, empID, currPay, ssn)
        {
            StockOptions = numbOfOpts;
        }

        public override void GiveBonus(float amount)
        {
            base.GiveBonus(amount);
            Random rnd = new Random();
            StockOptions += rnd.Next(500);
        }

        public override void DisplayStats()
        {
            base.DisplayStats();
            Console.WriteLine("Number of Stock Options: {0}", StockOptions);
        }
    }
}
