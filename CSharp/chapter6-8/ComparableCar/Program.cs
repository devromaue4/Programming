using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComparableCar
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with Object Sorting *****\n");
            Car[] cars = new Car[5];
            cars[0] = new Car("Rusty", 80, 1);
            cars[1] = new Car("Mary", 40, 234);
            cars[2] = new Car("Viper", 40, 34);
            cars[3] = new Car("Mel", 40, 4);
            cars[4] = new Car("Chucky", 40, 5);

            Console.WriteLine("Here is the unordered set of cars:");
            foreach (Car item in cars)
                Console.WriteLine("{0} {1}", item.CarID, item.PetName);

            // Отсортиовать по CarID
            Array.Sort(cars);
            Console.WriteLine();

            Console.WriteLine("Here is the ordered set of cars:");
            foreach (Car item in cars)
                Console.WriteLine("{0} {1}", item.CarID, item.PetName);

            Console.WriteLine();

            // Теперь отсортиовать по дружественному имени
            //Array.Sort(cars, new PetNameComparer());
            Array.Sort(cars, Car.SortByPetName);
            // Вывести отсортированный массив
            Console.WriteLine("Ordering by pet name:");
            foreach(Car item in cars)
                Console.WriteLine("{0} {1}", item.CarID, item.PetName);

            Console.ReadLine();
        }
    }
}
