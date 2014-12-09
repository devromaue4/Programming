using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaticDataAndMembers
{
    class SavingAccount
    {
        // Данные уровня экземпляра
        public double currBalance;
        // Сатический элемент данных
        public static double currInterestRate;// = 0.04;

        public SavingAccount(double balance)
        {
            currBalance = balance;
        }

        // Статический конструктор
        static SavingAccount()
        {
            Console.WriteLine("In static ctor!");
            currInterestRate = 0.04;
        }

        public static double GetInterestRate()
        {
            return currInterestRate;
        }

        public static void SetInterestRate(double newRate)
        {
            currInterestRate = newRate;
        }
    }
}
