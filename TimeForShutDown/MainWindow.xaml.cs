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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Timer myTimer;

        public MainWindow()
        {
            InitializeComponent();
            myTimer = new Timer();
            this.DataContext = myTimer;
           

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
