using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCSharpApp
{
    class Program
    {
        static int Main(string[] args)
        {
            Console.WriteLine("***** My First C# App *****");
            Console.WriteLine("Hello World!");
            Console.WriteLine();

            //for (int i = 0; i < args.Length; i++)
            //   Console.WriteLine("Arg: {0}", args[i]);
            
            //foreach (string arg in args)
            //    Console.WriteLine("Arg: {0}", arg);

            string[] theArgs = Environment.GetCommandLineArgs();
            foreach (string arg in theArgs)
                Console.WriteLine("Arg: {0}", arg);

            ShowEnvironmentDetails();

            Console.ReadLine();

            return Environment.ExitCode;// 0;
        }

        static void ShowEnvironmentDetails()
        {
            // Вывести информацию о дисковых устройствах
            // данной машины и другие интересные детали.
            foreach(string drive in Environment.GetLogicalDrives())
                Console.WriteLine("Drive: {0}", drive);

            Console.WriteLine("MachineName: {0}", Environment.MachineName);
            Console.WriteLine("UserName: {0}", Environment.UserName);

            if (Environment.Is64BitOperatingSystem)
                Console.WriteLine("OS: {0}: {1}", Environment.OSVersion, "64 bit");
            else
                Console.WriteLine("OS: {0}: {1}", Environment.OSVersion, "32 bit");

            Console.WriteLine("Number of Processors: {0}", Environment.ProcessorCount);
            Console.WriteLine(".NET Version: {0}", Environment.Version);
        }
    }
}
