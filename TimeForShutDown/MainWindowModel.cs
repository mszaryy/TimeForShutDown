using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Threading;

namespace TimeForShutDown
{
    class MainWindowModel : INotifyPropertyChanged
    {
       
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

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
                    if (value)
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


        private string startButtonText = "Start";
        public string StartButtonText
        {
            get { return this.startButtonText; }
            set
            {
                if (value != this.startButtonText)
                {
                    this.startButtonText = value;
                    NotifyPropertyChange("StartButtonText");
                }
            }
        }

       public RelayCommand ButtonCommand { get; private set; }
        private DispatcherTimer dispatcherTimer;
        private Utilities utilities;

        public MainWindowModel()
        {
            Timer();
            ButtonCommand = new RelayCommand(Button_Click);
            utilities = new Utilities();
        }

        public void Timer()
        {
            focusable = true;
            CountDown = true;
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Tick += Tick;
          }

        private void Tick(object sender, EventArgs e)
        {
            int tick = -1;
            if (ProcessEnd)
            {
                tick = 1;
            }
            StartTime = StartTime + new TimeSpan(0, 0, tick);
            if (StartTime == new TimeSpan(0, 0, 0))
            {
                CMD.Run("shutdown -s -f -t 0");
                dispatcherTimer.Stop();
            }
        }

        private void Button_Click(object sender)
        {
            if (StartButtonText == "Start")
            {
                TimeSpan currentTime = DateTime.Now.TimeOfDay;
                if ((startTime.CompareTo(currentTime) < 0) && turnOfAtTime == true)
                {
                    startTime = new TimeSpan(24, 0, 0).Subtract(currentTime).Add(StartTime);
                }
                else if (turnOfAtTime == true)
                {
                    startTime = startTime.Subtract(currentTime);
                }else if (ProcessEnd)
                {
                    utilities.WaitForProcessEnd(processName);
                    startTime = new TimeSpan(0, 0, 0);
                }
                dispatcherTimer.Start();
                StartButtonText = "Stop";
                Focusable = false;
            }
            else
            {
                Focusable = true;
                dispatcherTimer.Stop();
                if (processEnd)
                {
                    utilities.CancleBGW();
                }
                StartButtonText = "Start";
                CountDown = true;
            }
        }

       
    }
}
