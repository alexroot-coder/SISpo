using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Timers;

namespace lab2
{
    public class Program
    {
        public static int n = 0;
        public static void print_n()
        {
            Console.WriteLine(n);
            Thread.Sleep(1000);
        }


        public static void scan()
        {
            Thread firstThread = new Thread(new ThreadStart(print_n));
            firstThread.Start();
            if (Console.ReadKey().Key == ConsoleKey.DownArrow)
            {
                n++;
            }
        }

        public static void Main(string[] args)
        {

            Thread secondThread = new Thread(new ThreadStart(scan));
            secondThread.Start();
        }



    }
}
