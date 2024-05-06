using HelpApp.Data;
using HelpApp.Data.Models;
using HelpApp.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для PatientAdd.xaml
    /// </summary>
    public partial class PatientAdd : Window
    {
        public PatientAdd()
        {
            InitializeComponent();
            WindowsController.Registry(this);
            using (ApplicationContext context = new ApplicationContext())
            {
                foreach (var _diagnos in context.Diagnosis)
                {
                    diagnose.Items.Add(_diagnos.name);
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            WindowsController.SwitchTo<PatientsList>(this);
        }

        private bool CheckFields()
        {
            if (firstName.Text == "")
            {
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
            if (passport_id.Text == "")
            {
                return false;
            }
            if (country.Text == "")
            {
                return false;
            }
            if (city.Text == "")
            {
                return false;
            }
            if (street.Text == "")
            {
                return false;
            }
            if (appartments.Text == "")
            {
                return false;
            }
            if (building.Text == "")
            {
                return false;
            }
            if (email.Text == "")
            {
                return false;
            }
            return true;
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            if (CheckFields()) 
            {
                var _city = new City()
                {
                    name = city.Text
                };
                var _country = new Country()
                {
                    name = country.Text
                };
                var _street = new Street()
                {
                    name = street.Text
                };
                var addr = new Address()
                {
                    country = _country,
                    city = _city,
                    street = _street,
                    apartment = appartments.Text,
                    building = building.Text,
                };
                var passport = new Passport()
                {
                    address = addr,
                    birthday = birthday.DisplayDate,
                    first_name = firstName.Text,
                    second_name = secondName.Text,
                    third_name = thirdName.Text,
                    gender = male.IsChecked.Value ? "мужчина" : "женщина",
                    id = passport_id.Text
                };
                WindowsController.patientsListWindow.patients_container.Children.Clear();
                using (ApplicationContext context = new ApplicationContext())
                {
                    var d = context.Diagnosis.Where(_d => _d.name == diagnose.Text).First();
                    var p = new Patients()
                    {
                        diagnoses = [d],
                        email = email.Text,
                        passport = passport,
                        doctor = context.Doctors.Where(d => d.id == WindowsController.doctor.id).First(),
                    };
                    context.Patients.Add(p);
                    context.SaveChanges();
                    ClearFields();
                    foreach (var _patients in context.Patients.Where(p => p.doctor.id == WindowsController.doctor.id)
                        .Include(p => p.passport)
                    .Include(p => p.passport.address)
                    .Include(p => p.passport.address.city)
                    .Include(p => p.passport.address.country)
                    .Include(p => p.passport.address.street)
                    .Include(p => p.doctor)
                    .Include(p => p.diagnoses))
                    {
                        WindowsController.patientsListWindow.patients_container.Children.Add(new PatientNode(_patients));
                    }
                }
            }
            else error_text.Content = "Заполните все поля";
        }

        private void ClearFields()
        {
            firstName.Text = "";
            secondName.Text = "";
            thirdName.Text = "";
            passport_id.Text = "";
            country.Text = "";
            city.Text = "";
            street.Text = "";
            appartments.Text = "";
            building.Text = "";
            email.Text = "";
            birthday.Text = "";
            male.IsChecked = false;
            female.IsChecked = false;
        }
    }
}
