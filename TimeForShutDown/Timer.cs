using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;

namespace TimeForShutDown
{
    public class Timer : INotifyPropertyChanged
    {
        private string currentTime;
        public string CurrentTime
        {
            get { return this.currentTime; }
            set
            {
                if (value != this.currentTime)
                {
                    this.currentTime = value;
                    NotifyPropertyChange("CurrentTime");
                }
            }
        }
        private TimeSpan startTime;
        private DispatcherTimer timer;

        public Timer()
        {
            startTime = new TimeSpan(0, 0, 0);
            this.currentTime = String.Format("{0:hh\\:mm\\:ss}", startTime);
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

            startTime = startTime + new TimeSpan(0, 0, 1);
            CurrentTime = String.Format("{0:hh\\:mm\\:ss}", startTime);
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
