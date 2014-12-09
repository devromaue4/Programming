using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomEnumerator
{
    [Serializable]
    public class CarIsDeadException : ApplicationException
    {
        //private string messageDetails = string.Empty;
        public DateTime ErrorTimeStamp { get; set; }
        public string CauseOfError { get; set; }

        public CarIsDeadException() { }
        public CarIsDeadException(string message) : base(message) { }
        public CarIsDeadException(string message, System.Exception inner) : base(message, inner) { }
        protected CarIsDeadException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
        public CarIsDeadException(string message, string cause, DateTime time)
            : base(message)
        {
            CauseOfError = cause;
            ErrorTimeStamp = time;
        }


        //public override string Message
        //{
        //    get
        //    {
        //        return string.Format("Car Error Message: {0}", messageDetails);
        //    }
        //}
    }


    class Car
    {
        public const int MaxSpeed = 100;
        public int CurrentSpeed { get; set; }
        public string PetName { get; set; }

        private bool CarIsDead;

        private Radio theMusicBox = new Radio();

        public Car() { }
        public Car(string name, int speed)
        {
            PetName = name;
            CurrentSpeed = speed;
        }

        public void CrankTunes(bool state)
        {
            theMusicBox.TurnOn(state);
        }

        public void Accelerate(int delta)
        {
            if (CarIsDead)
                Console.WriteLine("{0} is out of order...", PetName);
            else
            {
                CurrentSpeed += delta;
                if (CurrentSpeed >= MaxSpeed)
                {
                    //Console.WriteLine("{0} has overheated!", PetName);
                    CurrentSpeed = 0;
                    CarIsDead = true;

                    //throw new Exception(string.Format("{0} has overhated!", PetName));

                    CarIsDeadException ex = new CarIsDeadException(string.Format("{0} has overhated!", PetName),
                        "You have a lead foot", DateTime.Now);
                    ex.HelpLink = "htpp://www.CarsRUs.com";
                    throw ex;
                }
                else
                    Console.WriteLine("=> CurrentSpeed = {0}", CurrentSpeed);
            }
        }
    }
}
