using HelpApp.Data;
using HelpApp.Utils;
using Microsoft.EntityFrameworkCore;
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
    /// Логика взаимодействия для Appointments.xaml
    /// </summary>
    public partial class Appointments : Window
    {
        public Appointments()
        {
            InitializeComponent();
            FillDataGrid();
        }

        public void FillDataGrid()
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                foreach (var _appointment in context.Appointment_records.Where(g => g.patient.id == WindowsController.currentPatient.id).Include(d=>d.doctor).Include(d=>d.hospital))
                {
                    StringBuilder doctors = new StringBuilder("");
                    var d = _appointment.doctor;
                    doctors.AppendLine($"{d.first_name} {d.second_name} {d.third_name}");
                    datagrid.Items.Add(new Data()
                    {
                        Hospital = _appointment.hospital.name,
                        Date = _appointment.date.ToString(),
                        Doctor = doctors.ToString()
                    });
                }
            }
        } 

        private class Data
        {
            public string Hospital { get; set; }
            public string Date { get; set; }
            public string Doctor { get; set; }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            WindowsController.SwitchTo<PatientCard>(this);
        }
    }
}
