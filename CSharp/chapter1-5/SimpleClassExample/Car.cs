using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleClassExample
{
    class Car
    {
        public string petName;
        public int currSpeed;

        // Как только определен уникальный конструктор с любым количеством параметров
        // стандартный консруктор удаляется

        // Вернуть стандартный конструктор который будет устанавливать 
        // для всех членов данных стандартные значения
        public Car() { }
        
        //public Car()
        //{
        //    petName = "Chuck";
        //    currSpeed = 10;
        //}

        // Здесь currSpeed получает стандартное значение для типа int (0)
        public Car(string pn)
        {
            petName = pn;
        }

        // Позволяетвызывающему коду установить полное состояние Car
        public Car(string pn, int cs )
        {
            petName = pn;
            currSpeed = cs;
        }

        public void PrintState()
        {
            Console.WriteLine("{0} is going {1} MPH.", petName, currSpeed);
        }

        public void SpeedUp(int delta)
        {
            currSpeed += delta;
        }
    }
}
