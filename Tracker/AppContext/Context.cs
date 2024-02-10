using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tracker.Controllers.DataBaseController.Models;
using Tracker.UserControls.Scope;

namespace Tracker.AppContext
{
    public class Context
    {
        public class GameContext
        {
            public enum MoveMethod
            {
                PROJECTION,
                CONTROLLER,
            }

            public double XAngle { get; set; }
            public double YAngle { get; set; }
            public int GameAreaWidth = 1280;
            public int GameAreaHeight = 720;
            public double XCenterAngle;
            public double YCenterAngle;
            public IPAddress address = IPAddress.Parse("192.168.150.1");
            public int port = 9998;
            public MoveMethod moveMethod = MoveMethod.PROJECTION;
        }

        public static UserModel User = new UserModel();
        public static GameContext Game = new GameContext();
        public static Scope scope;
    }
}
