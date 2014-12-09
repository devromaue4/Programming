using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithArrays
{
    class Program
    {
        static void Main(string[] args)
        {
            //SimpleArray();

            int[] iArr = new int[] { 10, 33, 12, 44 };
            PrintArray( iArr );

            string[] str = GetStringArray();
            foreach (string item in str)
                Console.WriteLine(item);

            Console.ReadLine();
        }

        static void SimpleArray()
        {
            Console.WriteLine("=> Simple Array Creation.");
            
            // Создать массив int, содержащий 3 элемента с индексами 0, 1, 2
            int[] myInts = new int[3];
            myInts[0] = 100;
            myInts[1] = 200;
            myInts[2] = 300;

            // Создать массив string содержащий 100 элементов с индексами 0 - 99
            string[] bookOnDotNet = new string[100];

            // Вывести эти значения
            foreach (int item in myInts)
                Console.WriteLine(item);

            // Синтаксис инициализации массива с использованием ключевого слова new
            string[] strArray = new string[] { "one", "two", "three" };
            Console.WriteLine("strArray has {0} elements", strArray.Length);

            // Синтаксис инициализации массива без использования ключевого слова new
            bool[] bArray = { true, false, true };
            Console.WriteLine("bArray has {0} elements", bArray.Length);

            // Синтаксис инициализации массива с использованием ключевого слова new и размера
            int[] iArray = new int[4] { 10, 20, 30, 40 };
            Console.WriteLine("iArray has {0} elements", iArray.Length);

            Console.WriteLine();
        }

        static void PrintArray(int[] iArray)
        {
            for (int i = 0; i < iArray.Length; i++)
                Console.WriteLine("Item {0} is {1}", i, iArray[i]);

        }

        static string[] GetStringArray()
        {
            string[] theStrings = { "Hello", "From", "GetStringArray" };
            return theStrings;
        }
    }
}
