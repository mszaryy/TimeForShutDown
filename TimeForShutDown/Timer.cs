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

        private bool turnOfAtTime;
        public bool TurnOfAtTime
        {
            get { return this.turnOfAtTime; }
            set
            {
                if (value != this.turnOfAtTime)
                {
                    this.turnOfAtTime = value;
                    if(value)
                    {
                        StartTime = DateTime.Now.TimeOfDay;
                    }                   
                    NotifyPropertyChange("TurnOfAtTime");
                }
            }
        }

        private bool countDown;
        public bool CountDown
        {
            get { return this.countDown; }
            set
            {
                if (value != this.countDown)
                {
                    this.countDown = value;
                    if (value)
                    {
                        StartTime = new TimeSpan(0, 15, 0);
                    }
                    NotifyPropertyChange("CountDown");
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

        private TimeSpan endTime;
        private DispatcherTimer timer;
        private Utilities utilities;

        public Timer()
        {
            StartTime = new TimeSpan(0, 15, 0);
            CountDown = true;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += tickTimer;
            utilities = new Utilities();
            Focusable = true;
        }
        public void Start()
        {
            Focusable = false;
            endTime = DateTime.Now.TimeOfDay;
            if ((startTime.CompareTo(endTime) < 0) && turnOfAtTime == true )
            {
                double seconds = endTime.TotalSeconds - (new TimeSpan(24,0,0)).TotalSeconds + startTime.TotalSeconds;
                StartTime = new TimeSpan(0,0, Convert.ToInt32(seconds));
            }else if(turnOfAtTime == true)
            {
                StartTime = startTime.Subtract(endTime);
            }

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
                CMD.Run("shutdown -s -t 0");
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
                timer.Stop();
            CountDown = true;

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
