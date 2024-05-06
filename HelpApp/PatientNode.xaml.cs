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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HelpApp
{
    /// <summary>
    /// Логика взаимодействия для PatientNode.xaml
    /// </summary>
    public partial class PatientNode : UserControl
    {
        public Patients patient;
        public PatientNode()
        {
            InitializeComponent();
        }

        public PatientNode(Patients patient)
        {
            InitializeComponent();
            this.patient = patient;
            text.Text = $"{patient.passport.second_name} {patient.passport.first_name} {patient.passport.third_name}";
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Background = Brushes.LightGray;
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Background = Brushes.White;
        }

        private void UserControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var window = Window.GetWindow(this);
            WindowsController.currentPatient = patient;
            WindowsController.SwitchTo<PatientCard>(window);
        }
    }
}
