using System.Windows;
using System.Windows.Controls;
using Tracker.Data;

namespace Tracker.UserControls
{
    /// <summary>
    /// Логика взаимодействия для LevelCreatingControl.xaml
    /// </summary>
    public partial class LevelCreatingControl : UserControl, IControl
    {
        public LevelCreatingControl()
        {
            InitializeComponent();
        }

        public string PageName { get { return "Создание уровней"; } }

        private void play_border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Window w = Window.GetWindow(sender as Border);
            w.Hide();
            MyWindowController.levelcreating().Show();
        }
    }
}
