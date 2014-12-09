using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleClassExample
{
    class Motorcycle
    {
        public int driverIntensity;
        public string driverName;

        //public Motorcycle() { }
        //public Motorcycle(int intensity) 
        //    : this(intensity, "") { }
        //public Motorcycle(string name)
        //    : this(0, name) { }

        //// main ctor
        //public Motorcycle(int intensity, string name)
        //{
        //    if (intensity > 10)
        //        intensity = 10;

        //    driverIntensity = intensity;
        //    driverName = name;
        //}

        // Единственный конструктор, использующий необязательные аргументы
        public Motorcycle(int intensity = 0, string name = "")
        {
            if (intensity > 10)
                intensity = 10;

            driverIntensity = intensity;
            driverName = name;
        }

        public void PopAWeheely()
        {
            for(int i = 0; i <= driverIntensity; i++)
                Console.WriteLine("Yeeeeeeeee Haaaaaaeewww!");
        }

        public void SetDriverName(string name)
        {
            driverName = name;
        }
    }
}
