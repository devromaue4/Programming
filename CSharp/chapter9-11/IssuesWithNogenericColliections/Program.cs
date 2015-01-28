using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace IssuesWithNogenericColliections
{
    class Persone
    {
        public int Age { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Persone() { }
        public Persone(string fname, string lname, int age)
        {
            FirstName = fname;
            LastName = lname;
            Age = age;
        }

        public override string ToString()
        {
            return string.Format("Name: {0} {1}, Age: {2}", FirstName, LastName, Age);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //simpleBoxUnboxOperation();

            List<Persone> morePeople = new List<Persone>();
            morePeople.Add(new Persone("Frank", "Black", 50));
            Console.WriteLine(morePeople[0]);

            List<int> moreInts = new List<int>();
            moreInts.Add(10);
            moreInts.Add(2);
            int sum = moreInts[0] + moreInts[1];
            Console.WriteLine(moreInts[0]);
            Console.WriteLine(moreInts[1]); 
            Console.WriteLine(sum);

           // testc code in thin 2 monitior egfgkfgfkgkfgkgdhhd
           //     ;
            Console.WriteLine("This new on my new code monitor ");

            // Error!
            //moreInts.Add(new Persone());

            Console.ReadLine();
        }

        private static void simpleBoxUnboxOperation()
        {
            int iVar = 25;

            // Упаковать int в ссылку на object
            object boxeInt = iVar;

            // Распаковать ссылку обратно в int
            try
            { 
                long unboxeInt = (long)boxeInt;
            }
            catch(InvalidCastException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
