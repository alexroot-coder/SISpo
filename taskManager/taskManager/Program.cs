using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace taskManager
{
    class TaskObj {
        public String taskName;
        public int taskPID;
        public double taskCPU;
        public double taskRAM;

        private  DateTime lastTime;
        private TimeSpan lastTotalProcessorTime;
        private DateTime curTime;
        private TimeSpan curTotalProcessorTime;

        public TaskObj(String _taskName, int _taskPID, double _taskRAM)
        {
            this.taskName = _taskName;
            this.taskPID = _taskPID;
            this.taskRAM = _taskRAM;
            this.taskCPU = 0.0;
        }

        public void CpuUsage()
        {
            var result_cpu_usage = 0.0;
            Process[] processListFromCPUUsage = Process.GetProcessesByName(taskName);

            if (processListFromCPUUsage.Length != 0)
            {
                Process processFromCPUUsage = processListFromCPUUsage[0];
                if (this.lastTime == null || this.lastTime == new DateTime())
                {
                    this.lastTime = DateTime.Now;
                    this.lastTotalProcessorTime = processFromCPUUsage.TotalProcessorTime;
                }
                else
                {
                    this.curTime = DateTime.Now;
                    this.curTotalProcessorTime = processFromCPUUsage.TotalProcessorTime;

                    double CPUUsage = (this.curTotalProcessorTime.TotalMilliseconds - this.lastTotalProcessorTime.TotalMilliseconds) 
                        / this.curTime.Subtract(lastTime).TotalMilliseconds 
                        / Convert.ToDouble(Environment.ProcessorCount);
                    this.lastTime = this.curTime;
                    this.lastTotalProcessorTime = this.curTotalProcessorTime;
                    this.taskCPU = (CPUUsage * 100);
                }
            }
            else
            {
                this.taskCPU = result_cpu_usage;
            }

        }
    }
    class Program
    {
        public static Process[] listOfProcesses = Process.GetProcesses();
        public static List<Process> tempProcessesList;
        private static List<TaskObj> listOfTask = new();
        private static List<TaskObj> sortedList = new();
        public static int iter = 0;
        public static int processesLength = listOfProcesses.Length;
        public static bool sortByName = true;
        public static bool sortByPID = false;
        public static bool sortByRAM = false;
        public static bool sortByCPU_USAGE = false;

        internal static List<TaskObj> ListOfTask { get => listOfTask; set => listOfTask = value; }

        static int Compare(int a, int b)
        {
            if (a > b)
            {
                return b;
            }
            if (b > a)
            {
                return a;
            }
            if (a == b)
            {
                return a;
            }
            return 0;
        }
        static void output()
        {
            while (true)
            {
                Console.CursorVisible = false;
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("{0,-5}   {1,-35} {2,-5} \t {3,-7}      {4,-20}", "№", "Name", "Ram", "PID", "CPU");
                Console.WriteLine("-------------------------------------------------------------------------------------------");

                getInfo();

                if (sortByName == true) sortedList = ListOfTask.OrderBy(X => X.taskName).ToList();
                

                if (sortByPID == true) sortedList = ListOfTask.OrderBy(X => X.taskPID).ToList();
                

                if (sortByRAM == true) sortedList = ListOfTask.OrderBy(X => X.taskRAM).ToList();

                if (sortByCPU_USAGE == true) sortedList = ListOfTask.OrderBy(X => X.taskCPU).ToList(); 



                for (int i = iter; i < Compare(iter + 28, sortedList.Count); i++)
                {

                    sortedList[i].CpuUsage();
                    var otsv = sortedList[i].taskCPU;
 
                    Console.WriteLine("{0,-5}   {1,-35} {2,-5}M \t {3,-7} \t {4,-6:0.00}%", i, sortedList[i].taskName, sortedList[i].taskRAM, sortedList[i].taskPID, otsv) ;
                }
                Thread.Sleep(10);
            }
        }

        static void getInfo()
        {
            ListOfTask.Clear();

            listOfProcesses = Process.GetProcesses();
            tempProcessesList = listOfProcesses.ToList();

            for(int i = 1; i < tempProcessesList.Count; i++)
            {
                TaskObj task = new(
                        tempProcessesList[i].ProcessName,
                        tempProcessesList[i].Id,
                        tempProcessesList[i].PrivateMemorySize64 / 1000000L
                        );

                task.CpuUsage();
                ListOfTask.Add(task);
            }

        }
       


        static void Main(string[] args)
        {
            Console.Title = "TaskManager";
            Console.SetWindowSize(90, 31);
            var taskManager = new Thread(output);
            taskManager.Start();

            while (true)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.UpArrow)
                {
                    if (iter > 0)
                    {
                        iter--;
                    }
                }
                if (key == ConsoleKey.DownArrow)
                {
                    if (iter < processesLength)
                    {
                        iter++;
                    }

                    if (iter > processesLength - 28)
                    {
                        iter = processesLength - 28;
                    }
                }

                if (key == ConsoleKey.A)
                {
                    sortByName = true;
                    sortByPID = false;
                    sortByRAM = false;
                    sortByCPU_USAGE = false;
                }

                if (key == ConsoleKey.S)
                {
                    sortByName = false;
                    sortByPID = false;
                    sortByRAM = true;
                    sortByCPU_USAGE = false;
                }
                if (key == ConsoleKey.D)
                {
                    sortByName = false;
                    sortByPID = true;
                    sortByRAM = false;
                    sortByCPU_USAGE = false;
                }

                if (key == ConsoleKey.F)
                {
                    sortByName = false;
                    sortByPID = false;
                    sortByRAM = false;
                    sortByCPU_USAGE = true;
                }


                if (key == ConsoleKey.Escape)
                {
                    taskManager.Abort();
                    break;
                }
            }
        }
    }
}
