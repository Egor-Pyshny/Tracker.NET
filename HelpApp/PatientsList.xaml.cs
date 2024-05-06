using HelpApp.Data.Models;
using HelpApp.Data;
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
using System.Numerics;
using Microsoft.EntityFrameworkCore;

namespace HelpApp
{
    /// <summary>
    /// Логика взаимодействия для PatientsList.xaml
    /// </summary>
    public partial class PatientsList : Window
    {
        public PatientsList()
        {
            InitializeComponent();
            WindowsController.Registry(this);
            using (ApplicationContext context = new ApplicationContext())
            {
                foreach (var _patients in context.Patients.Where(p => p.doctor.id == WindowsController.doctor.id)
                    .Include(p => p.passport)
                    .Include(p => p.passport.address)
                    .Include(p => p.passport.address.city)
                    .Include(p => p.passport.address.country)
                    .Include(p => p.passport.address.street)
                    .Include(p => p.doctor)
                    .Include(p => p.diagnoses))
                {
                    patients_container.Children.Add(new PatientNode(_patients));
                }
            }
        }

        private void filter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && filter.Text.Length>0)
            {
                patients_container.Children.Clear();
                using (ApplicationContext context = new ApplicationContext())
                {
                    foreach (var _patients in context.Patients.Where(
                        p => p.doctor.id == WindowsController.doctor.id && p.passport.second_name.StartsWith(filter.Text))
                        .Include(p => p.passport)
                    .Include(p => p.passport.address)
                    .Include(p => p.passport.address.city)
                    .Include(p => p.passport.address.country)
                    .Include(p => p.passport.address.street)
                    .Include(p => p.doctor)
                    .Include(p => p.diagnoses))
                    {
                        patients_container.Children.Add(new PatientNode(_patients));
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WindowsController.SwitchTo<PatientAdd>(this);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            WindowsController.CloseAll(this);
        }

        private void filter_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (filter.Text.Length == 0)
            {
                patients_container.Children.Clear();
                using (ApplicationContext context = new ApplicationContext())
                {
                    foreach (var _patients in context.Patients.Where(
                        p => p.doctor.id == WindowsController.doctor.id && p.passport.second_name.StartsWith(filter.Text))
                        .Include(p => p.passport)
                    .Include(p => p.passport.address)
                    .Include(p => p.passport.address.city)
                    .Include(p => p.passport.address.country)
                    .Include(p => p.passport.address.street)
                    .Include(p => p.doctor)
                    .Include(p => p.diagnoses))
                    {
                        patients_container.Children.Add(new PatientNode(_patients));
                    }
                }
            }
        }
    }
}
