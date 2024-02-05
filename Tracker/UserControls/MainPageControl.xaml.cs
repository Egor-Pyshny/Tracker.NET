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

namespace Tracker.UserControls
{
    /// <summary>
    /// Логика взаимодействия для MainPageControl.xaml
    /// </summary>
    public partial class MainPageControl : UserControl
    {
        public string name = "Main Page";

        public MainPageControl()
        {
            InitializeComponent();
        }

        private void play_border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Window w = Window.GetWindow(sender as Border);
            (w as MainWindow).SwitchToGameWindow();
        }
    }
}
