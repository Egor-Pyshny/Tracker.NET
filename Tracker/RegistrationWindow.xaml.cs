using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Tracker.Data;

namespace Tracker
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>

    public class PersonModel : IDataErrorInfo
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Position { get; set; }
        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "Age":
                        if ((Age < 0) || (Age > 100))
                        {
                            error = "Возраст должен быть больше 0 и меньше 100";
                        }
                        break;
                    case "Name":
                        //Обработка ошибок для свойства Name
                        break;
                    case "Position":
                        //Обработка ошибок для свойства Position
                        break;
                }
                return error;
            }
        }
        public string Error
        {
            get { throw new NotImplementedException(); }
        }
    }

    public partial class RegistrationWindow : Window
    {
        private bool registration = true;
        private TextBox __name_box, __email_box;
        private PasswordBox __passw_box, __passw_check_box;
        private CheckBox __agreement_box;
        PersonModel Tom;
        public RegistrationWindow()
        {
            InitializeComponent();
            MyWindowController.register(this);

            Tom = new PersonModel();
            this.DataContext = Tom;
            __name_box = this.name_field;
            __email_box = this.email_field;
            __passw_box = this.password_field;
            __passw_check_box = this.passw_check_field;
            __agreement_box = this.agreement;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void SwitchToSignUpForm(object sender, RoutedEventArgs e)
        {
            if (!registration)
            {
                registration = true;
                __name_box.Clear();
                __passw_check_box.Clear();
                this.email_field.Clear();
                this.password_field.Clear();
                registaration_fields.Children.Insert(0, __name_box);
                registaration_fields.Children.Insert(3, __passw_check_box);
                registaration_fields.Children.Insert(4, __agreement_box);
                this.sign_up_btn.Background = new SolidColorBrush(Color.FromRgb(0x05, 0xB7, 0x90));
                this.log_in_btn.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                this.auto_enter.IsChecked = false;
                this.agreement.IsChecked = false;
            }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
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

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void MinimizedIcon_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Button).Background = null;
        }

        private void SwitchToLogInForm(object sender, RoutedEventArgs e)
        {
            if (registration)
            {
                registration = false;
                this.name_field.Clear();
                this.email_field.Clear();
                this.password_field.Clear();
                this.passw_check_field.Clear();
                this.auto_enter.IsChecked = false;
                this.agreement.IsChecked = false;
                registaration_fields.Children.Remove(__name_box);
                registaration_fields.Children.Remove(__passw_check_box);
                registaration_fields.Children.Remove(__agreement_box);
                this.sign_up_btn.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                this.log_in_btn.Background = new SolidColorBrush(Color.FromRgb(0x05, 0xB7, 0x90));
                this.auto_enter.IsChecked = false;
            }
        }
    }
}
