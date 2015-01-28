using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace MyExtensionMethods//ExensionMethods
{
    static class MyExtensions
    {
        // Этот метод позоляет любому объекту отобразить
        // сборку, в которой он определен.
        public static void DisplayDefiningAssembly(this object obj)
        {
            Console.WriteLine("{0} lives here: => {1}\n", obj.GetType().Name,
                Assembly.GetAssembly(obj.GetType()).GetName().Name);
        }

        // Этот метод позволяет любому целому числу изменить порядок следования
        // десятичных цифр на обратный. Например, 56 превратится 65.
        public static int ReverseDigits(this int i)
        {
            // Транслировать int в String и затем получить все его символы.
            char[] digits = i.ToString().ToCharArray();

            // Изменить порядок элементов массива.
            Array.Reverse(digits);

            // Вставить обратно в строку.
            string newDigits = new string(digits);

            // Вернуть модифицированную строку как int.
            return int.Parse(newDigits);
        }
    }
}
