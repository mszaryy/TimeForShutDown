using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        public MainWindow()
        {
            InitializeComponent();
            processComboBox.ItemsSource = Utilities.GetProcessList();
            InitHideIcon();
        }

        public void InitHideIcon()
        {
            System.Windows.Forms.NotifyIcon notifyIcon = new System.Windows.Forms.NotifyIcon();
            notifyIcon.Icon = new System.Drawing.Icon(Directory.GetCurrentDirectory() + @"\shutdownIco.ico");
            notifyIcon.Visible = true;
            notifyIcon.DoubleClick += new EventHandler(NotifyIconn);
        }

        private void NotifyIconn(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = WindowState.Normal;
        }

        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == System.Windows.WindowState.Minimized)
            {
                this.Hide();
            }
            base.OnStateChanged(e);
        }
    }

}
