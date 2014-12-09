using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees
{
    partial class Employee
    {
        // Fields
        protected string empName;
        protected int empID;
        protected float currPay;
        protected int empAge;
        protected string empSSN;

        public class BenefitPakage
        {
            public enum BenefitPakageLevel
            {
                Standart, Gold, Platinum
            }

            public double ComputePayDeduction()
            {
                return 125.0;
            }
        }

        // ctor's
        public Employee() { }
        public Employee(string name, int id, float pay)
            : this(name, 0, id, pay, "") { }
        public Employee(string name, int age, int id, float pay, string ssn)
        {
            Name = name;
            ID = id;
            Age = age;
            Pay = pay;
            empSSN = ssn;
        }
    }
}
