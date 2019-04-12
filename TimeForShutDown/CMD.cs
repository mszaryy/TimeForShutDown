using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TimeForShutDown
{
    abstract class CMD
    {
        public static void Run(string command)
        {
            //Process process = new Process();
            //process.StartInfo.FileName = Path.Combine(Environment.SystemDirectory, "cmd.exe");
            //process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //process.StartInfo.Arguments = "/c " + command;
            //process.Start();

             MessageBox.Show("shutdown!");
        }
    }
}
