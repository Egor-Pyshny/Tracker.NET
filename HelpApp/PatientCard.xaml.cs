using HelpApp.Data;
using HelpApp.Data.Models;
using HelpApp.Utils;
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
using System.Windows.Shapes;

namespace HelpApp
{
    /// <summary>
    /// Логика взаимодействия для PatientCard.xaml
    /// </summary>
    public partial class PatientCard : Window
    {
        public Label selected_diagnos = null;
        public PatientCard()
        {
            InitializeComponent();
            WindowsController.Registry(this);
            using (ApplicationContext context = new ApplicationContext())
            {
                foreach (var _diagnos in context.Diagnosis)
                {
                    diagnose.Items.Add(_diagnos.name);
                }
                foreach (var _diagnos in WindowsController.currentPatient.diagnoses)
                {
                    AddLabel(_diagnos);
                }
                passport_id.Content = "Идентификатор паспорта: " + WindowsController.currentPatient.passport.id;
                fio.Content = $"ФИО: {WindowsController.currentPatient.passport.second_name} " +
                    $"{WindowsController.currentPatient.passport.first_name} " +
                    $"{WindowsController.currentPatient.passport.third_name}";
                birthday.Content = "Дата рождения: "+WindowsController.currentPatient.passport.birthday.Date;
                gender.Content = "Пол:"+WindowsController.currentPatient.passport.gender;
                address.Content = $"Адрес: " +
                    $"{WindowsController.currentPatient.passport.address.country.name} " +
                    $"{WindowsController.currentPatient.passport.address.city.name} " +
                    $"{WindowsController.currentPatient.passport.address.street.name} " +
                    $"{WindowsController.currentPatient.passport.address.building} " +
                    $"{WindowsController.currentPatient.passport.address.apartment} ";
                email.Content = "Почта: " + WindowsController.currentPatient.email;
            }
        }

        private void add_diagnos_Click(object sender, RoutedEventArgs e)
        {
            Diagnosis diagnos;
            using (ApplicationContext context = new ApplicationContext())
            {
                diagnos = context.Diagnosis.Where(d => d.name == diagnose.Text).First();
                context.Patients.Where(p => p.id == WindowsController.currentPatient.id).First().diagnoses.Add(diagnos);
                context.SaveChanges();
            }
            diagnose.SelectedIndex = -1;
            AddLabel(diagnos);
        }

        private void AddLabel(Diagnosis diagnos)
        {
            var temp = new Label();
            temp.Content = diagnos.name;
            temp.Height = 40;
            temp.FontSize = 20;
            temp.MouseLeftButtonUp += SetActiveDiagnos;
            temp.MouseEnter += UserControl_MouseEnter;
            temp.MouseLeave += UserControl_MouseLeave;
            diagnoses_container.Children.Add(temp);
        }

        private void SetActiveDiagnos(object sender, MouseButtonEventArgs e)
        {
            if(selected_diagnos != null)
                selected_diagnos.Background = Brushes.White;
            selected_diagnos = (Label)sender;
            (sender as Label).Background = Brushes.LightGray;
        }

        private void remove_diagnos_Click(object sender, RoutedEventArgs e)
        {
            if (selected_diagnos != null)
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    var diagnos = context.Diagnosis.Where(d => d.name == selected_diagnos.Content).First();
                    context.Patients.Where(p=>p.id == WindowsController.currentPatient.id).First().diagnoses.Remove(diagnos);
                    context.SaveChanges();
                    diagnoses_container.Children.Clear();
                    foreach (var _diagnos in context.Patients.Where(p => p.id == WindowsController.currentPatient.id).First().diagnoses)
                    {
                        AddLabel(_diagnos);
                    }
                }
            }
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Label).Background = Brushes.LightGray;
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Label).Background = Brushes.White;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WindowsController.SwitchTo<Additionalnfo>(this);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            WindowsController.SwitchTo<Operations>(this);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            WindowsController.SwitchTo<Appointments>(this);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            WindowsController.SwitchTo<PatientsList>(this);
        }
    }
}
