using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambdaExpressionMultipleParams
{
    class SimpleMath
    {
        public delegate void MathMessage(string msg, int result);
        private MathMessage mmDelegate;

        public void SetMathHandler(MathMessage target) { mmDelegate = target; }

        public void Add(int x, int y)
        {
            if (mmDelegate != null)
                mmDelegate("Adding has completed!", x + y);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Зарегестрировать делегат как лямбда-выражение
            SimpleMath math = new SimpleMath();
            math.SetMathHandler((msg, result) => 
            { 
                Console.WriteLine("Message: {0}, Result: {1}", msg, result); 
            });

            // Это приведет к выполнению лямбда-выражения
            math.Add(10, 10);

            Console.ReadLine();
        }
    }
}
