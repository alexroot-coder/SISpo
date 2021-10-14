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
            while (true)
            {
               Console.WriteLine(n);
                Thread.Sleep(1000);
                
            }
        }

        static void secondThrd()
        {
            var first = new Thread(firstThrd);
            first.Start();

            while (true)    
            {
                
                var tt = Console.ReadKey().KeyChar;
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
                
                if (Console.ReadKey().Key == ConsoleKey.OemPlus)
                {
                    n++;
                }
                if (Console.ReadKey().Key == ConsoleKey.OemMinus)
                {
                    n--;
                }
                
            }
        }
 
    }
}
