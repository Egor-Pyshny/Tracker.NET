using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Tracker.AppContext;
using Tracker.Controllers.DataBaseController;
using Tracker.Controllers.DataBaseController.Models;
using Tracker.Data;

namespace Tracker
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary

    public partial class RegistrationWindow : Window
    {

        private bool registration = true;
        
        public RegistrationWindow()
        {
            InitializeComponent();
            MyWindowController.register(this);
            this.DataContext = Context.User;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private bool Validate() {
            bool res = true;
            if (registration && (Context.User.DateOfBirth = birth_date.ToString()) == "") {
                res = false;
                birth_date.BorderBrush = Brushes.Red;
            }
            try
            {
                if (!(Regex.IsMatch(Context.User.FirstName, @"^[А-Я]*$")))
                {
                    res = false;
                    first_name.BorderBrush = Brushes.Red;
                }
            } catch(ArgumentNullException) {
                res = false;
                first_name.BorderBrush = Brushes.Red;
            }

            try
            {
                if (!(Regex.IsMatch(Context.User.SecondName, @"^[А-Я]*$")))
                {
                    res = false;
                    second_name.BorderBrush = Brushes.Red;
                }
            }
            catch (ArgumentNullException)
            {
                res = false;
                second_name.BorderBrush = Brushes.Red;
            }

            try
            {
                if (!(Regex.IsMatch(Context.User.ThirdName, @"^[А-Я]*$")))
                { 
                    res = false;
                    third_name.BorderBrush = Brushes.Red;
                }
            }
            catch (ArgumentNullException)
            {
                res = false;
                third_name.BorderBrush = Brushes.Red;
            }

            if (registration)
            {
                try
                {
                    if (!(Regex.IsMatch(Context.User.Description, @"^[А-Я]*$")))
                    {
                        res = false;
                        disease_field.BorderBrush = Brushes.Red;
                    }
                }
                catch (ArgumentNullException)
                {
                    res = false;
                    disease_field.BorderBrush = Brushes.Red;
                }
            }

            if (res) {
                if (registration) {
                    if (!(MyDBController.RegisterNewUser(Context.User)))
                    {
                        res = false;
                        error_msg.Text = "Пользователь с таким именем уже зарегистрирован";
                    }
                } else if (!(MyDBController.CheckUserExists(Context.User)))
                {
                    res = false;
                    error_msg.Text = "Пользователь с таким именем не зарегистрирован";
                }
            }
            return res;
        }

        private void SwitchToMain() {
            this.Hide();
            MyWindowController.main().Show();
        }

        private void SwitchToSignUpForm(object sender, RoutedEventArgs e)
        {
            if (!registration)
            {
                error_msg.Text = "";
                registration = true;
                first_name.Clear();
                first_name.BorderBrush = new SolidColorBrush(Color.FromRgb(0xC5,0xC8,0xCC));
                second_name.Clear();
                second_name.BorderBrush = new SolidColorBrush(Color.FromRgb(0xC5, 0xC8, 0xCC));
                third_name.Clear();
                third_name.BorderBrush = new SolidColorBrush(Color.FromRgb(0xC5, 0xC8, 0xCC));
                birth_date.Visibility = Visibility.Visible;
                birth_date.BorderBrush = new SolidColorBrush(Color.FromRgb(0xC5, 0xC8, 0xCC));
                disease_field.Visibility = Visibility.Visible;
                disease_field.Clear();
                disease_field.BorderBrush = new SolidColorBrush(Color.FromRgb(0xC5, 0xC8, 0xCC));
                auto_enter.IsChecked = false;
                sign_up_btn.Background = new SolidColorBrush(Color.FromRgb(0x05, 0xB7, 0x90));
                log_in_btn.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            }
            else if(Validate())
            {
                SwitchToMain();
            }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            MyWindowController.Close(this);
        }

        private void BtnClose_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Button).Background = new SolidColorBrush(Color.FromRgb(255, 0, 0));
        }

        private void BtnClose_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Button).Background = null;
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

        private void MinimizedIcon_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Button).Background = null;
        }

        private void SwitchToLogInForm(object sender, RoutedEventArgs e)
        {
            if (registration)
            {
                error_msg.Text = "";
                registration = false;
                first_name.Clear();
                first_name.BorderBrush = new SolidColorBrush(Color.FromRgb(0xC5, 0xC8, 0xCC));
                second_name.Clear();
                second_name.BorderBrush = new SolidColorBrush(Color.FromRgb(0xC5, 0xC8, 0xCC));
                third_name.Clear();
                third_name.BorderBrush = new SolidColorBrush(Color.FromRgb(0xC5, 0xC8, 0xCC));
                birth_date.Visibility = Visibility.Collapsed;
                disease_field.Visibility = Visibility.Collapsed;
                auto_enter.IsChecked = false;
                sign_up_btn.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                log_in_btn.Background = new SolidColorBrush(Color.FromRgb(0x05, 0xB7, 0x90));
            }
            else if (Validate())
            {
                SwitchToMain();
            }
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                textBox.Text = textBox.Text.ToUpper();
                textBox.CaretIndex = textBox.Text.Length;
            }
        }
    }
}
