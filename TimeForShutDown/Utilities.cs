using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace TimeForShutDown
{
     class Utilities
    {
        private BackgroundWorker bGW;
        public static List<string> GetProcessList()
        {
            List<string> process_ = new List<string>();
            foreach (Process item in Process.GetProcesses())
            {
                if (!process_.Contains(item.ProcessName))
                {
                    process_.Add(item.ProcessName);
                }
            }
            return process_.OrderBy(x => x).ToList();
        }



        public void WaitForProcessEnd(string processName)
        {
            bGW = new BackgroundWorker();
            bGW.DoWork += new DoWorkEventHandler(bGWDoWork);
            bGW.RunWorkerCompleted += bGW_RunWorkerCompleted;
            bGW.RunWorkerAsync(argument: processName);
            bGW.WorkerSupportsCancellation = true;
        }

        public void CancleBGW()
        {
            bGW.CancelAsync();
        }

        private void bGW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("shutdown!");
        }

        private void bGWDoWork(object sender, DoWorkEventArgs e)
        {
            string procName = (string)e.Argument;
            do
            {
                System.Threading.Thread.Sleep(2000);
            } while (Process.GetProcessesByName("Calculator").Length > 0);
        }

    }
}
