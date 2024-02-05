using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using Tracker.Data;
using Tracker.UserControls;
using Tracker.UserControls.Scope;

namespace Tracker
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int Max = 50;
        private bool opened = false;
        public Dictionary<string, UserControl> controls = new Dictionary<string, UserControl>
        {
            { "ItemPlay", new MainPageControl()},
            { "ItemSettings", new SettingsWindowControl()},
        };
        public UserControl control;
        public MainWindow()
        {
            InitializeComponent();
            MyWindowController.register(this);
            control = controls["ItemPlay"];
            Reciver.Start();
            content_grid.Children.Clear();
            content_grid.Children.Add(control);
        }

        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void BtnMinimize_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Button).Background = new SolidColorBrush(Color.FromRgb(55, 55, 55));
        }

        private void BtnMinimize_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Button).Background = null;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            MyWindowController.Close(this);
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            opened = true;
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            opened = false;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }

        private void BtnClose_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Button).Background = new SolidColorBrush(Color.FromRgb(255, 0, 0));
        }

        private void BtnClose_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Button).Background = null;
        }

        private void ButtonOpenMenu_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Button).Background = new SolidColorBrush(Color.FromRgb(55, 55, 55));
            (sender as Button).Opacity = 0.5;
        }

        private void ButtonOpenMenu_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Button).Background = null;
            (sender as Button).Opacity = 1;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Reciver.Stop();
            (controls["ItemSettings"] as SettingsWindowControl).Stop();
            App.Current.Shutdown();
        }

        private void taskbar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        public void SwitchToGameWindow()
        {
            this.Hide();
            Scope.step = (int)(controls["ItemSettings"] as MainPageControl).sens_slider.Value;
            GameWindow game = MyWindowController.game();
            game.Width = 1280;
            game.Height = 720 + 60;
            //game.OnGameStart();
            game.Show();
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string page = ((ListViewItem)((ListView)sender).SelectedItem)?.Name;
            if (controls.ContainsKey(page))
            {
                if (control is SettingsWindowControl) (control as SettingsWindowControl).StopReciving();
                control = controls[((ListViewItem)((ListView)sender).SelectedItem)?.Name];
                if (page == "ItemSettings") (control as SettingsWindowControl).StartReciving();
                current_page.Text = control.Name.Replace("_", " ");
                content_grid.Children.Clear();
                content_grid.Children.Add(control);
            }
            else {
                Console.WriteLine("in progress...");
            }
        }

        private void ListViewMenu_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (opened)
            {
                RoutedEventArgs eventArgs = new RoutedEventArgs(ButtonBase.ClickEvent, ButtonCloseMenu);
                ButtonCloseMenu.RaiseEvent(eventArgs);
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (opened)
            {
                RoutedEventArgs eventArgs = new RoutedEventArgs(ButtonBase.ClickEvent, ButtonCloseMenu);
                ButtonCloseMenu.RaiseEvent(eventArgs);
            }
        }

        private void ListViewExit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Console.WriteLine("logout");
        }
    }
}
