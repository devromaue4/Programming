using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithEnums
{
    enum EmpType //: byte
    {
        Manager = 102,
        Grunt,
        Contractor,
        VicePresident
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Создать тип подрядчика
            EmpType emp = EmpType.Contractor;
            DayOfWeek day = DayOfWeek.Monday;
            ConsoleColor cc = ConsoleColor.Gray;
            //AskForBonus(emp);

            // Вывести тип хранилища, используемый в перечислении
            //Console.WriteLine("EmpType uses a {0} for storage", Enum.GetUnderlyingType(emp.GetType()));
            //Console.WriteLine("EmpType uses a {0} for storage", Enum.GetUnderlyingType(typeof(EmpType)));
           // Console.WriteLine("emp is a {0}", emp.ToString());
            //Console.WriteLine("emp is a {0} = {1}", emp.ToString(), (int)emp);

            EvaluateEnum(emp);
            EvaluateEnum(day);
            EvaluateEnum(cc);

            Console.ReadLine();
        }

        // Использовать перечисления в качестве параметров
        static void AskForBonus(EmpType e)
        {
            switch(e)
            {
                case EmpType.Manager:
                    Console.WriteLine("Не желаете ли взамен фондовые опционы?");
                    break;
                case EmpType.Grunt:
                    Console.WriteLine("Вы, наверное, шутите...");
                    break;
                case EmpType.Contractor:
                    Console.WriteLine("Вы уже получаете вполне достаточно...");
                    break;
                case EmpType.VicePresident:
                    Console.WriteLine("VERY GOOD, sir!");
                    break;
            }
        }

        // Этот метод выводит детали любого перечисления
        static void EvaluateEnum(System.Enum e)
        {
            Console.WriteLine("=> Information about {0}", e.GetType().Name);
            Console.WriteLine("Underlying storage type: {0}", Enum.GetUnderlyingType(e.GetType()));

            // Получить все пары имя/значение для входного параметра
            Array enumData = Enum.GetValues(e.GetType());
            Console.WriteLine("This enum has {0} members", enumData.Length);

            // Вывести строковое имя и ассоциированное знчение,
            // используя флаг формата D (см. главу 3)
            for (int i = 0; i < enumData.Length; i++)
            {
                Console.WriteLine("Name: {0},  Value: {0:D}", enumData.GetValue(i));
            }

            Console.WriteLine();
        }
    }
}
