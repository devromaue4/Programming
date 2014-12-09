using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDelegate
{
    class Program
    {delegate void CarEngineHandler2(string msgForCaller);

        static void Main(string[] args)
        {
            Console.WriteLine("***** Delegates as event enablers *****\n");

            Car c1 = new Car("SlugBug", 100, 10);
            // vesion 1
            c1.RegisterWithCarEngine(OnCarEngineEvent);
            c1.RegisterWithCarEngine(OnCarEngineEvent2);
            // vesion 2
            //c1.RegisterWithCarEngine(new Car.CarEngineHandler(OnCarEngineEvent));
            //Car.CarEngineHandler handCallEv2 = new Car.CarEngineHandler(OnCarEngineEvent2);
            //c1.RegisterWithCarEngine(handCallEv2);

            Console.WriteLine("***** Speeding up *****");
            for (int i = 0; i < 6; i++)
                c1.Accelerate(20);

            // vesion 1
            c1.UnRegisterWithCarEngine(OnCarEngineEvent2);
            // vesion 2
            //c1.UnRegisterWithCarEngine(handCallEv2);

            Console.WriteLine("***** Speeding up *****");
            for (int i = 0; i < 6; i++)
                c1.Accelerate(20);

            Console.ReadLine();
        }

        public static void OnCarEngineEvent(string msg)
        {
            Console.WriteLine("\n***** Message From Car Object *****");
            Console.WriteLine("=> {0}", msg);
            Console.WriteLine("***********************************\n");
        }

        public static void OnCarEngineEvent2(string msg)
        {
            Console.WriteLine("=> {0}", msg.ToUpper());
        }
    }
}
