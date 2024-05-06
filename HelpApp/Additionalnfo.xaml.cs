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

namespace HelpApp
{
    /// <summary>
    /// Логика взаимодействия для Additionalnfo.xaml
    /// </summary>
    public partial class Additionalnfo : Window
    {
        public Additionalnfo()
        {
            InitializeComponent();
            FillDataGrid();
        }

        public void FillDataGrid()
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                foreach (var res in context.General_urine_analysis.Where(g => g.patient.id == WindowsController.currentPatient.id))
                {
                    StringBuilder doctors = new StringBuilder("");
                    datagrid.Items.Add(new Data()
                    {
                        AnalysType = "Анализ мочи",
                        Date = res.date.ToString(),
                        Path = res.file_path
                    });
                }
                foreach (var res in context.General_blood_analysis.Where(g => g.patient.id == WindowsController.currentPatient.id))
                {
                    StringBuilder doctors = new StringBuilder("");
                    datagrid.Items.Add(new Data()
                    {
                        AnalysType = "Анализ мочи",
                        Date = res.date.ToString(),
                        Path = res.file_path
                    });
                }
                foreach (var res in context.Xray_results.Where(g => g.patient.id == WindowsController.currentPatient.id))
                {
                    StringBuilder doctors = new StringBuilder("");
                    datagrid.Items.Add(new Data()
                    {
                        AnalysType = "Анализ мочи",
                        Date = res.date.ToString(),
                        Path = res.file_path
                    });
                }
                foreach (var res in context.Game_history.Where(g => g.patient.id == WindowsController.currentPatient.id))
                {
                    StringBuilder doctors = new StringBuilder("");
                    datagrid.Items.Add(new Data()
                    {
                        AnalysType = "Результаты игры",
                        Date = res.date.ToString(),
                        Path = res.file_path
                    });
                }
            }
        }

        private class Data
        {
            public string AnalysType { get; set; }
            public string Date { get; set; }
            public string Path { get; set; }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            WindowsController.SwitchTo<PatientCard>(this);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Data data = (Data)datagrid.SelectedItem;
            if (data != null)
            {

            }
        }
    }
}
