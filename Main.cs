using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace FlucProcs
{
    class Program
    {
        static void Main(string[] args)
        {


            List<int> lastPros = Process.GetProcesses().Select((x) => x.Id).ToList();
            Process[] OldProcessList = Process.GetProcesses();
            List<Process> pro = new List<Process>();
            while (true)
            {
                Process[] processlist = Process.GetProcesses();

                List<int> CurrentPros = processlist.Select((x) => x.Id).ToList();
                List<int> diff = lastPros.Except(CurrentPros).ToList();
                Console.ForegroundColor = ConsoleColor.Red;

                pro = OldProcessList.Where((x) => diff.Contains(x.Id)).ToList();

                if (diff.Count == 0)
                {
                    pro = processlist.Where((x) => diff.Contains(x.Id)).ToList();
                    diff = CurrentPros.Except(lastPros).ToList();
                    Console.ForegroundColor = ConsoleColor.Green;
                    pro = processlist.Where((x) => diff.Contains(x.Id)).ToList();
                }
                foreach (int oldPID in diff)
                {
                    Console.Write("PID: {0}", oldPID);
                    try
                    {
                        Console.WriteLine(" | Name: {0}", pro.Where((x) => x.Id == oldPID).ToList()[0].ProcessName);
                    }
                    catch
                    {
                    }

                }
                if (diff.Count > 0)
                {
                    lastPros = CurrentPros;
                    OldProcessList = processlist;
                }
                System.Threading.Thread.Sleep(100);
            }

        }
    }
}
