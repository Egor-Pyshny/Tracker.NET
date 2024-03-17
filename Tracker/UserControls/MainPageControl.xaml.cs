using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Tracker.UserControls
{
    /// <summary>
    /// Логика взаимодействия для MainPageControl.xaml
    /// </summary>
    public partial class MainPageControl : UserControl, IControl
    {
       

        public MainPageControl()
        {
            InitializeComponent();
        }

        public string PageName { get { return "Основная страница"; } }

        private void play_border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Window w = Window.GetWindow(sender as Border);
            (w as MainWindow).SwitchToGameWindow();
        }
    }
}
