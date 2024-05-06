using HelpApp.Data.Models;
using HelpApp.Data;
using HelpApp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using Microsoft.EntityFrameworkCore;

namespace HelpApp
{
    /// <summary>
    /// Логика взаимодействия для Operations.xaml
    /// </summary>
    public partial class Operations : Window
    {
        public Operations()
        {
            InitializeComponent();
            /*datagrid.Items.Add(new Data()
            {
                Hospital = "_surgery.hospital.name",
                Date =" _surgery.date.ToString()",
                Doctors = "doctors.ToString()",
                Report = "будет доступен после операции"
            });*/
            FillDataGrid();
        }

        public void FillDataGrid()
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                foreach (var _surgery in context.Surgeries_graphic.Where(g => g.patient.id == WindowsController.currentPatient.id).Include(p=>p.Doctors).Include(p => p.hospital))
                {
                    StringBuilder doctors = new StringBuilder("");
                    foreach (var d in _surgery.Doctors)
                    {
                        doctors.AppendLine($"{d.first_name} {d.second_name} {d.third_name}");
                    }
                    datagrid.Items.Add(new Data() { 
                        Hospital = _surgery.hospital.name,
                        Date = _surgery.date.ToString(),
                        Doctors = doctors.ToString(),
                        Report = "будет доступен после операции"
                    });
                }
                foreach (var _surgery in context.Surgeries_history.Where(g => g.patient.id == WindowsController.currentPatient.id).Include(p => p.doctors).Include(p => p.surgery_Report).Include(p => p.hospital))
                {
                    StringBuilder doctors = new StringBuilder("");
                    foreach (var d in _surgery.doctors)
                    {
                        doctors.AppendLine($"{d.first_name} {d.second_name} {d.third_name}");
                    }
                    datagrid.Items.Add(new Data()
                    {
                        Hospital = _surgery.hospital.name,
                        Date = _surgery.date.ToString(),
                        Doctors = doctors.ToString(),
                        Report = _surgery.surgery_Report.file_path
                    });
                }
            }
        }

        private class Data
        { 
            public string Hospital { get; set; }
            public string Date { get; set; }
            public string Doctors { get; set; }
            public string Report { get; set; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Data data = (Data)datagrid.SelectedItem;
            if (data != null)
            { 
                
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            WindowsController.SwitchTo<PatientCard>(this);
        }
    }
}
