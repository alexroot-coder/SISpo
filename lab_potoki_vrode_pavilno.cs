using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace ConsoleApplication1
{

    internal class Program
    {
        public static int n = 0;

        public static void Main(string[] args)
        {
            var second = new Thread(secondThrd);
            second.Start();

        }
        static void firstThrd()
        {
            var np = 0;
            while (true)
            {
               // if (np != n)
                //{
                 //   Console.SetCursorPosition(0, 0);
                  //  Console.WriteLine(n);
                   // np = n;
                //}
                Console.WriteLine(n);
                Thread.Sleep(1000);
            }
        }

        static void secondThrd()
        {
            var first = new Thread(firstThrd);
            first.Start();
            
 


            do {
                while (!Console.KeyAvailable)
                {

                    var tt = Console.ReadKey(true).Key;

                    if (tt == ConsoleKey.OemPlus)
                    {
                        n++;
                    }

                    if (tt == ConsoleKey.OemMinus)
                    {
                        n--;
                    }
                }

            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
            first.Abort();
           
        }

    }
}
