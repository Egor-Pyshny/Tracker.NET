using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tracker.Controllers.DataBaseController;
using Tracker.UserControls;

namespace Tracker.Data
{
    public class MyWindowController
    {
        public static Window _registration;
        public static Window _main;
        public static Window _levelcreating;
        public static Window _game;

        public static void register(Window w) {
            switch (w.GetType().Name) {
                case "MainWindow":
                    _main = w;
                    break;
                case "RegistrationWindow":
                    _registration = w;
                    break;
                case "LevelCreatingWindow":
                    _levelcreating = w;
                    break;
                case "GameWindow":
                    _game = w;
                    break;
                default:
                    throw new Exception("unknown class");
            }
        }

        public static RegistrationWindow registration() {
            if(_registration == null) new RegistrationWindow();
            return (RegistrationWindow)_registration;
        }

        public static MainWindow main()
        {
            if (_main == null) new MainWindow();
            return (MainWindow)_main;
        }

        public static LevelCreatingWindow levelcreating()
        {
            if (_levelcreating == null) new LevelCreatingWindow();
            return (LevelCreatingWindow)_levelcreating;
        }

        public static GameWindow game()
        {
            if (_game == null) new GameWindow();
            return (GameWindow)_game;
        }

        public static void Close(Window sender) {
            if (_registration != null && !(sender is RegistrationWindow)) _registration.Close();
            if (_levelcreating != null) _levelcreating.Close();
            if (_main != null)
            {
                ((_main as MainWindow).controls["ItemSettings"] as SettingsWindowControl).Stop();         
                if(!(sender is MainWindow)) _main.Close();
            }
            if (_game != null) {
                (_game as GameWindow).Stop();
                if (!(sender is GameWindow)) _game.Close();
            }
            Reciver.Stop();
            MyDBController.Stop();
            App.Current.Shutdown();
        }
    }
}
