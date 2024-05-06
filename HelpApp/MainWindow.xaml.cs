using HelpApp.Data;
using HelpApp.Data.Models;
using HelpApp.Utils;
using System.Numerics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HelpApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isSignIn = false;
        public MainWindow()
        {
            InitializeComponent();
            WindowsController.Registry(this);
            using (ApplicationContext context = new ApplicationContext())
            {
                foreach (var _hospital in context.Hospitals)
                { 
                    hospital.Items.Add(_hospital.name);
                }
            }
        }

        private void signIn_Click(object sender, RoutedEventArgs e)
        {
            if (!isSignIn) 
            { 
                isSignIn = true;
                hospital.Visibility = Visibility.Collapsed;
                hospital_lbl.Visibility = Visibility.Collapsed;
            }
            else
            {
                if (CheckFields()) 
                {
                    Doctors? doctor = FindDoctor();
                    if (doctor != null)
                    {
                        WindowsController.doctor = doctor;
                        WindowsController.SwitchTo<PatientsList>(this);
                    }
                    else
                    {
                        error_text.Content = "Нет такого врача";
                    }
                }
                else error_text.Content = "Заполните все поля";
            }
        }

        private void registraation_Click(object sender, RoutedEventArgs e)
        {
            if (isSignIn)
            {
                isSignIn = false;
                hospital.Visibility = Visibility.Visible;
                hospital_lbl.Visibility = Visibility.Visible;
            }
            else
            {
                if (CheckFields())
                {
                    Doctors doctor = GetDoctor();
                    using (ApplicationContext context = new ApplicationContext())
                    {
                        doctor.hospital = context.Hospitals.Where(_hospital => _hospital.name == hospital.Text).FirstOrDefault();
                        context.Doctors.Add(doctor);
                        WindowsController.doctor = doctor;
                        context.SaveChanges();
                        WindowsController.SwitchTo<PatientsList>(this);
                    }
                }
                else error_text.Content = "Заполните все поля";
            }
        }

        private bool CheckFields() {
            if (firstName.Text == "") {
                return false;
            }
            if (secondName.Text == "")
            {
                return false;
            }
            if (thirdName.Text == "")
            {
                return false;
            }
            if (!isSignIn && hospital.SelectedIndex == -1)
            {
                return false;
            }
            return true;
        }
        
        private Doctors GetDoctor()
        {
            return new Doctors()
            { 
                first_name = firstName.Text,
                second_name = secondName.Text,
                third_name = thirdName.Text,
            };
        }
        
        private Doctors? FindDoctor()
        {
            Doctors? doctor = null;
            using (ApplicationContext context = new ApplicationContext())
            {
                doctor = context.Doctors.Where(
                    _doctor =>
                    _doctor.first_name == firstName.Text &&
                    _doctor.second_name == secondName.Text &&
                    _doctor.third_name == thirdName.Text
                    ).FirstOrDefault();
            }
            return doctor;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            WindowsController.CloseAll(this);
        }
    }
}