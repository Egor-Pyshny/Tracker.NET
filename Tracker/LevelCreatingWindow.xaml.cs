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
using Tracker;
using Tracker.Data;
using Tracker.UserControls.Targets;

namespace Tracker
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class LevelCreatingWindow : Window
    {
        private UserControl target;
        private int target_number = 1;
        public LevelCreatingWindow()
        {
            InitializeComponent();
            MyWindowController.register(this);
        }

        private void Target_MouseDoubleClickEvent(object sender, MouseButtonEventArgs e)
        {

        }

        private void BtnClose_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Button).Background = new SolidColorBrush(Color.FromRgb(255, 0, 0));
        }

        private void BtnClose_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Button).Background = null;
        }

        private void BtnMinimize_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Button).Background = new SolidColorBrush(Color.FromRgb(55, 55, 55));
        }

        private void BtnMinimize_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Button).Background = null;
        }

        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void taskbar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void ListViewTargets_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListViewItem item = null;
            if ((sender as ListView).SelectedItems.Count == 2)
                (sender as ListView).SelectedItems.RemoveAt(0);
            if ((sender as ListView).SelectedItems.Count != 0)
                item = (ListViewItem)((sender as ListView).SelectedItems[0]);
            switch (item?.Name)
            {
                case "_5section":
                    target = new _5SectionTargetControl();
                    target.Name = "Target" + target_number;
                    target_number++;
                    (target as ITarget).mode = Mode.DESIGN;
                    target.Margin = new Thickness(0, 0, 980, 420);
                    content_grid.Children.Add(target);
                    (sender as ListView).SelectedItems.Clear();
                    break;
                case "_4section":
                    (sender as ListView).SelectedItems.Clear();
                    break;
                case "_3section":
                    break;
                    (sender as ListView).SelectedItems.Clear();
                default:
                    break;
            }
        }
    }
}
