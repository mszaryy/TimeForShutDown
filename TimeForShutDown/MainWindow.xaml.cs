using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TimeForShutDown
{
    public partial class MainWindow : Window
    {
        private Timer myTimer;

        public MainWindow()
        {
            InitializeComponent();
            processComboBox.ItemsSource = Utilities.GetProcessList();
            myTimer = new Timer();
            this.DataContext = myTimer;
            initHideIcon();


        }
        
        public void initHideIcon()
        {
            System.Windows.Forms.NotifyIcon notifyIcon = new System.Windows.Forms.NotifyIcon();
            notifyIcon.Visible = true;
            notifyIcon.DoubleClick += new EventHandler(notifyIconn);
        }

        private void notifyIconn(object sender, EventArgs e)
        {
            this.Show();    
            this.WindowState = WindowState.Normal;
        }

        protected override void OnStateChanged(EventArgs e)
        {
            if(WindowState == System.Windows.WindowState.Minimized)
            {
                this.Hide();
            }
            base.OnStateChanged(e);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button Name = (sender as Button);
            if(Name.Content.ToString() == "Start")
            {
                myTimer.Start();
                Name.Content = "Stop";
            }
            else
            {
                myTimer.Stop();
                Name.Content = "Start";
            }           
        }

      
    }

}
