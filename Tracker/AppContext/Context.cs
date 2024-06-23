using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tracker.Controllers.DataBaseController.Models;
using Tracker.UserControls.Scope;

namespace Tracker.AppContext
{
    public class Context
    {
        public class GameContext : INotifyPropertyChanged
        {
            public enum MoveMethod
            {
                PROJECTION,
                CONTROLLER,
            }

            public double XAngle { get; set; }
            public double YAngle { get; set; }
            public int GameAreaWidth = 900;
            public int GameAreaHeight = 900;
            public double XCenterAngle;
            public double YCenterAngle;
            public IPAddress address = IPAddress.Parse("192.168.150.2");
            public int port = 9998;
            public MoveMethod moveMethod = MoveMethod.PROJECTION;

            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public static UserModel User = new UserModel();
        public static GameContext Game = new GameContext();
        public static Scope scope;
    }
}
