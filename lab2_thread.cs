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
                if (np != n)
                {
                    Console.WriteLine(n);
                    np = n;
                }
               // Thread.Sleep(0.1);

            }
        }

        static void secondThrd()
        {
            var first = new Thread(firstThrd);
            first.Start();

            while (true)
            {

                var tt = Console.ReadKey(true).KeyChar;
                //var tt = _getch();
                if (tt == 'б')
                {
                    first.Abort();
                    break;
                }
                if (tt == 'e')
                {
                    first.Abort();
                    break;
                }

                if (Console.ReadKey(true).Key == ConsoleKey.OemPlus)
                {
                    n++;
                }
                if (Console.ReadKey(true).Key == ConsoleKey.OemMinus)
                {
                    n--;
                }

            }
        }

    }
}
