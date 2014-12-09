using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeConversion
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with  type conversions *****");

            // Добавить две переменные типа short и высти результат.
            //short numb1 = 30000, numb2 = 30000;
            //short answer = (short)Add(numb1, numb2);
            //byte myByte = 0;
            //int myInt = 200;
            //myByte = (byte)myInt;
            //Console.WriteLine(answer);
            //Console.WriteLine(myByte);

            ProcessBytes();

            Console.ReadLine();
        }

        static int Add( int x, int y )
        {
            return x + y;
        }

        static void ProcessBytes()
        {
            byte v1 = 100;
            byte v2 = 250;

            // На этот раз сообщить компилятору о необходимости добавления
            // CIL-кода, необходимого для генерации исключения, если возникает
            // переполнение или потеря значимости.
            try
            {
                checked
                {
                    byte sum = (byte)Add(v1, v2);
                    Console.WriteLine("sum = {0}", sum);
                }
            }
            catch(OverflowException ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            
        }
    }
}
