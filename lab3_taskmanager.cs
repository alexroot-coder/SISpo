using System;
using System.Diagnostics;
using System.Threading;
using static System.Threading.Thread;

namespace taskmngr
{
    internal class Program
    {   
        public static Process[] list_of_processes = Process.GetProcesses();
        public static int iter = 0;
        public static int total_processes_length = list_of_processes.Length;
        
        static int Compare(int a, int b)
        {
            if(a > b)
            {
                return b;
            }
            if(b > a)
            {
                return a;
            }
            if(a == b)
            {
                return a;
            }
            return 0;
        }
        
        public static void Main(string[] args)
        {
            Console.Title = "TaskManager";
            Console.SetWindowSize(120, 30);
            var first_thrd = new Thread(first_output);
            first_thrd.Start();
            while (true)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.UpArrow)
                {
                    if(iter > 0)
                    {
                        iter--;
                    }
                }
                if (key == ConsoleKey.DownArrow)
                {
                    if (iter < total_processes_length)
                    {
                        iter++;
                    }

                    if (iter > total_processes_length-28)
                    {
                        iter = total_processes_length-28;
                    }
                }
                if(key == ConsoleKey.Escape)
                {
                    first_thrd.Abort();
                    break;
                }

            }

        }

        static void first_output()
        {
            while (true)
            {   
                Console.CursorVisible = false;
                Console.SetCursorPosition(0,0);
                //Console.Clear();
                Console.WriteLine("{0,-5}   {1,-35} {2,-25}  {3,-7}      {4,-7}","№", "Name", "Ram" , "PID","CPU");
                list_of_processes = Process.GetProcesses();
                total_processes_length = list_of_processes.Length;
                Console.WriteLine("-------------------------------------------------------------------------------------------");
 
                for (int i = iter; i < Compare(iter + 28 , total_processes_length); i++)
                {
                    Console.WriteLine("{0,-5}   {1,-35} {2,-25}  {3,-7}  ",i, list_of_processes[i].ProcessName, list_of_processes[i].PrivateMemorySize64 , list_of_processes[i].Id );
                }
                //Sleep(650);
            }
        }
    }
}
