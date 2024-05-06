using HelpApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HelpApp.Utils
{
    public static class WindowsController
    {
        public static MainWindow? mainWindow = null;
        public static PatientCard? cardWindow = null;
        public static PatientsList? patientsListWindow = null;
        public static PatientAdd? patientAdd = null;
        public static Additionalnfo? info = null;
        public static Operations? operations = null;
        public static Appointments? appointments = null;
        public static Doctors doctor;
        public static Patients currentPatient;
        public static void Registry(Window window) { 
            if (window is MainWindow)
                mainWindow = (MainWindow)window;
            else if (window is PatientCard)
                cardWindow = (PatientCard)window;
            else if (window is PatientsList)
                patientsListWindow = (PatientsList)window;
            else if (window is PatientAdd)
                patientAdd = (PatientAdd)window;
            else if (window is Additionalnfo)
                info = (Additionalnfo)window;
            else if (window is Operations)
                operations = (Operations)window;
            else if (window is Appointments)
                appointments = (Appointments)window;
            //InitializeAll();
        }

        private static void InitializeAll()
        {
            if (mainWindow == null)
                mainWindow = new MainWindow();
            if (cardWindow == null)
                cardWindow = new PatientCard();
            if (patientsListWindow == null)
                patientsListWindow = new PatientsList();
            if (patientAdd == null)
                patientAdd = new PatientAdd();
        }

        public static void SwitchTo<TWindow>(Window window)
        {
            Type type = typeof(TWindow);
            if (type == typeof(MainWindow))
                if (mainWindow != null)
                    mainWindow.Visibility = Visibility.Visible;
                else
                {
                    mainWindow = new MainWindow();
                    mainWindow.Visibility = Visibility.Visible;
                }
            else if (type == typeof(PatientCard))
                if (cardWindow != null)
                    cardWindow.Visibility = Visibility.Visible;
                else
                {
                    cardWindow = new PatientCard();
                    cardWindow.Visibility = Visibility.Visible;
                }
            else if (type == typeof(PatientsList))
                if (patientsListWindow != null)
                    patientsListWindow.Visibility = Visibility.Visible;
                else
                {
                    patientsListWindow = new PatientsList();
                    patientsListWindow.Visibility = Visibility.Visible;
                }
            else if (type == typeof(PatientAdd))
                if (patientAdd != null)
                    patientAdd.Visibility = Visibility.Visible;
                else
                {
                    patientAdd = new PatientAdd();
                    patientAdd.Visibility = Visibility.Visible;
                }
            else if (type == typeof(Additionalnfo))
                if (info != null)
                    info.Visibility = Visibility.Visible;
                else
                {
                    info = new Additionalnfo();
                    info.Visibility = Visibility.Visible;
                }
            else if (type == typeof(Operations))
                if (operations != null)
                    operations.Visibility = Visibility.Visible;
                else
                {
                    operations = new Operations();
                    operations.Visibility = Visibility.Visible;
                }
            else if (type == typeof(Appointments))
                if (appointments != null)
                    appointments.Visibility = Visibility.Visible;
                else
                {
                    appointments = new Appointments();
                    appointments.Visibility = Visibility.Visible;
                }
            window.Visibility = Visibility.Collapsed;
        }

        public static void CloseAll(Window window) 
        {
            if (window is MainWindow)
            {
                if (cardWindow != null) cardWindow!.Close();
                if (patientsListWindow != null) patientsListWindow!.Close();
                if (patientAdd != null) patientAdd!.Close();
                if (info != null) info.Close();
                if (appointments != null) appointments!.Close();
                if (operations != null) operations!.Close();
            }
            else if (window is PatientCard)
            {
                if (mainWindow != null) mainWindow!.Close();
                if (patientsListWindow != null) patientsListWindow!.Close();
                if (patientAdd != null) patientAdd!.Close();
                if (info != null) info.Close();
                if (appointments != null) appointments!.Close();
                if (operations != null) operations!.Close();
            }
            else if (window is PatientsList)
            {
                if (mainWindow != null) mainWindow!.Close();
                if (cardWindow != null) cardWindow!.Close();
                if (patientAdd != null) patientAdd!.Close();
                if (info != null) info.Close();
                if (appointments != null) appointments!.Close();
                if (operations != null) operations!.Close();
            }
            else if (window is PatientAdd)
            {
                if (mainWindow != null) mainWindow!.Close();
                if (cardWindow != null) cardWindow!.Close();
                if (patientsListWindow != null) patientsListWindow!.Close();
                if (info != null) info.Close();
                if (operations != null) operations!.Close();
                if (appointments != null) appointments!.Close();
            }
            else if (window is Additionalnfo)
            {
                if (mainWindow != null) mainWindow!.Close();
                if (cardWindow != null) cardWindow!.Close();
                if (patientsListWindow != null) patientsListWindow!.Close();
                if (patientAdd != null) patientAdd!.Close();
                if (appointments != null) appointments!.Close();
                if (operations != null) operations!.Close();
            }
            else if (window is Operations)
            {
                if (mainWindow != null) mainWindow!.Close();
                if (cardWindow != null) cardWindow!.Close();
                if (patientsListWindow != null) patientsListWindow!.Close();
                if (patientAdd != null) patientAdd!.Close();
                if (appointments != null) appointments!.Close();
                if (info != null) info.Close();
            }
            else if (window is Appointments)
            {
                if (mainWindow != null) mainWindow!.Close();
                if (cardWindow != null) cardWindow!.Close();
                if (patientsListWindow != null) patientsListWindow!.Close();
                if (patientAdd != null) patientAdd!.Close();
                if (operations != null) operations!.Close();
                if (info != null) info.Close();
            }
        }
    }
}
