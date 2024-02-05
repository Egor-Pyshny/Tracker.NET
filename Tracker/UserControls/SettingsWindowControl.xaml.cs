using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tracker.Data;
using Tracker.UserControls.Scope;

namespace Tracker.UserControls
{
    /// <summary>
    /// Логика взаимодействия для SettingsWindowControl.xaml
    /// </summary>
    public partial class SettingsWindowControl : UserControl
    {
        private bool calibrating_x = false;
        private bool calibrating_y = false;
        private bool calibrating_center = false;
        private bool center_calibrated = false;
        private bool runnig = false;
        private Thread thread;
        private Point3D p;
        public string name = "Settings Page";

        public SettingsWindowControl()
        {
            InitializeComponent();
            runnig = true;
            thread = new Thread(ThreadFunc);
            thread.Start();
        }

        private void ThreadFunc() { 
            while (runnig)
            {
                p = Reciver.p;
                if (center_calibrated)
                {
                    p.X -= Scope.Scope.x_center;
                    p.Y -= Scope.Scope.y_center;
                }
                if (calibrating_x) {
                    Scope.Scope.max_x = p.X;
                    Dispatcher.Invoke(() =>
                    {
                        x_textbox.Text = p.X.ToString("F5");
                    });
                }
                if (calibrating_y)
                {
                    Scope.Scope.max_y = p.Y;
                    Dispatcher.Invoke(() =>
                    {
                        y_textbox.Text = p.Y.ToString("F5");
                    });
                }
                if (calibrating_center) {
                    Scope.Scope.x_center = p.X;
                    Scope.Scope.y_center = p.Y;
                }
            }
        }

        public void Stop() {
            runnig = false;
            thread.Join();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void x_calibrating_Click(object sender, RoutedEventArgs e)
        {
            if (!calibrating_x)
            {
                calibrating_x = true;
                (sender as Button).SetValue(ButtonProgressAssist.IsIndeterminateProperty, true);
                (sender as Button).Content = "Stop calibrating X";
            }
            else
            {
                Console.WriteLine("max_x = " + Scope.Scope.max_x.ToString());
                calibrating_x = false;
                (sender as Button).SetValue(ButtonProgressAssist.IsIndeterminateProperty, false);
                (sender as Button).Content = "Start calibrating X";
            }
        }

        private void center_calibrating_Click(object sender, RoutedEventArgs e)
        {
            if (!calibrating_center)
            {
                calibrating_center = false;
                calibrating_center = true;
                (sender as Button).SetValue(ButtonProgressAssist.IsIndeterminateProperty, true);
                (sender as Button).Content = "Stop calibrating center";
            }
            else
            {
                Console.WriteLine("x = "+Scope.Scope.x_center.ToString());
                Console.WriteLine("y = "+Scope.Scope.y_center.ToString());
                calibrating_center = true;
                calibrating_center = false;
                (sender as Button).SetValue(ButtonProgressAssist.IsIndeterminateProperty, false);
                (sender as Button).Content = "Start calibrating center";
            }
        }

        private void y_calibrating_Click(object sender, RoutedEventArgs e)
        {
            if (!calibrating_y)
            {
                calibrating_y = true;
                (sender as Button).SetValue(ButtonProgressAssist.IsIndeterminateProperty, true);
                (sender as Button).Content = "Stop calibrating Y";
            }
            else
            {
                Console.WriteLine("max_y = " + Scope.Scope.max_y.ToString());
                calibrating_y = false;
                (sender as Button).SetValue(ButtonProgressAssist.IsIndeterminateProperty, false);
                (sender as Button).Content = "Start calibrating Y";
            }
        }

        private void set_default_Click(object sender, RoutedEventArgs e)
        {
            ip_txtb.Text = "192.168.0.0";
            port_txtb.Text = "9998";
        }

        public void StopReciving()
        {
            runnig = false;
        }

        public void StartReciving()
        {
            runnig = true;
        }
    }
}
