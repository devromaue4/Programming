using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDelegate
{
    public class Car
    {
        public int CurrentSpeed { get; set; }
        public int MaxSpeed { get; set; }
        public string PetName { get; set; }

        private bool carIsDead;

        public delegate void CarEngineHandler(string msgForCaller);
        private CarEngineHandler listOfHandlers;

        // Функция которая позволяет вызывающему коду указыать метод для обратного вызова
        public void RegisterWithCarEngine(CarEngineHandler methodToCall)
        {
            listOfHandlers += methodToCall;
        }
        public void UnRegisterWithCarEngine(CarEngineHandler methodToCall)
        {
            listOfHandlers -= methodToCall;
        }

        public Car() { MaxSpeed = 100;  }
        public Car(string name, int maxSpeed, int currSpeed)
        {
            CurrentSpeed = currSpeed;
            MaxSpeed = maxSpeed;
            PetName = name;
        }

        // Реализовать метод Accelerate() для обращения
        // к списку вызовов делегата при нужных условиях
        public void Accelerate(int delta)
        {
            // Если автомобиль сломан, отправить сообщение об этом
            if(carIsDead)
            {
                if (listOfHandlers != null)
                    listOfHandlers("Sorry, this car is dead...");
            }
            else
            {
                CurrentSpeed += delta;
                // Автомобиль почти сломан?
                if(10 == (MaxSpeed - CurrentSpeed) && listOfHandlers != null)
                {
                    listOfHandlers("Careful buddy! Gonna blow!");
                }

                if (CurrentSpeed >= MaxSpeed)
                    carIsDead = true;
                else
                    Console.WriteLine("CurrentSpeed = {0}", CurrentSpeed);
            }
        }
    }
}
