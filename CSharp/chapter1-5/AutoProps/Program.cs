using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoProps
{
    class Car
    {
        // Автоматические свойства должны быть доступны для чтения и для записи
        public string PetName { get; set; }
        public int Speed { get; set; }
        public string Color { get; set; }

        public void DisplaStats()
        {
            Console.WriteLine("Car Name: {0}", PetName);
            Console.WriteLine("Car Sped: {0}", Speed);
            Console.WriteLine("Car Color: {0}", Color);
        }
    }

    class Garage
    {
        // Скрытое поле int установлено в 0
        public int NumOfCars { get; set; }
        // Скрытое поле Car установлено в null
        public Car MyAuto { get; set; }

        public Garage()
        {
            NumOfCars = 1;
            MyAuto = new Car();
        }

        public Garage(Car car, int numCars = 1)
        {
            MyAuto = car;
            NumOfCars = numCars;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with Automatic Properties *****\n");
            Car c = new Car();
            c.PetName = "Frank";
            c.Speed = 55;
            c.Color = "Red";
            c.DisplaStats();

            Garage gar = new Garage(c);
            //gar.MyAuto = c;
            Console.WriteLine("Number of cars: {0}", gar.NumOfCars);
            Console.WriteLine("You car is named: {0}", c.PetName);

            Console.ReadLine();
        }
    }
}
