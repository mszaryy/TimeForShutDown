using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;

namespace TimeForShutDown
{
    public class Timer : INotifyPropertyChanged
    {
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

        public Timer()
        {
            StartTime = new TimeSpan(0, 15, 0);
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += tickTimer;
        }

        public void Start()
        {
            timer.Start();
        }

        private void tickTimer(object sender, EventArgs e)
        {
            StartTime = StartTime - new TimeSpan(0, 0, 1);
            if (startTime == new TimeSpan(0, 0, 0))
            {
                MessageBox.Show("ShutDown");
                timer.Stop();
            }
        }

        public void Stop()
        {
            timer.Stop();
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
