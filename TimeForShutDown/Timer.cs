using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;

namespace TimeForShutDown
{
    public class Timer : INotifyPropertyChanged
    {
        private bool focusable;
        public bool Focusable
        {
            get { return this.focusable; }
            set
            {
                if (value != this.focusable)
                {
                    this.focusable = value;
                    NotifyPropertyChange("Focusable");
                }
            }
        }

        private bool processEnd;
        public bool ProcessEnd
        {
            get { return this.processEnd; }
            set
            {
                if (value != this.processEnd)
                {
                    this.processEnd = value;
                    NotifyPropertyChange("ProcessEnd");
                }
            }
        }
        private string processName;
        public string ProcessName
        {
            get { return this.processName; }
            set
            {
                if (value != this.processName)
                {
                    this.processName = value;
                    NotifyPropertyChange("ProcessName");
                }
            }
        }
        private TimeSpan startTime;
        public TimeSpan StartTime
        {
            get { return this.startTime; }
            set
            {
                if (value != this.startTime)
                {
                    this.startTime = value;
                    NotifyPropertyChange("StartTime");
                }
            }
        }

        private DispatcherTimer timer;
        private Utilities utilities;

        public Timer()
        {
            StartTime = new TimeSpan(0, 15, 0);
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += tickTimer;
            utilities = new Utilities();
            Focusable = true;
        }
        public void Start()
        {
            Focusable = false;
            timer.Start();
            if (processEnd)
            {
                utilities.WaitForProcessEnd(processName);
                StartTime = new TimeSpan(0, 0, 0);
            } 
                
            
            
        }
        private void tickTimer(object sender, EventArgs e)
        {
            int tick = -1;
            if(processEnd)
            {
                tick = 1;
            }
            StartTime = StartTime + new TimeSpan(0, 0, tick);
            if (startTime == new TimeSpan(0, 0, 0))
            {
                MessageBox.Show("ShutDown");
                timer.Stop();
            }
        }

        public void Stop()
        {
            Focusable = true;
            if (ProcessEnd)
            {
                utilities.CancleBGW();
            }
            else
            {
                timer.Stop();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChange(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }


    }
}
