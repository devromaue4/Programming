using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleClassExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with Class Types *****\n");
            
            // Вызов стандартного конструктора
            //Car chuck = new Car();
            //chuck.PrintState();
            //Car mary = new Car("Mary");
            //mary.PrintState();
            //Car daisy = new Car("Daisy", 75);
            //daisy.PrintState();

            //myCar.petName = "Henry";
           // myCar.currSpeed = 10;

            // Увеличить скорость автомобиля в несколько раз и вывести новое состояние
            //for(int i = 0; i <= 10; i++)
            //{
            //    myCar.SpeedUp(5);
            //    myCar.PrintState();
            //}

            Motorcycle MotoC = new Motorcycle(5);
            MotoC.SetDriverName("Tiny");
            MotoC.PopAWeheely();
            Console.WriteLine("Rider name is {0}", MotoC.driverName);

            Console.ReadLine();
        }
    }
}
