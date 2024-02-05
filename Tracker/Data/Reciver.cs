using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace Tracker.Data
{
    public static class Reciver
    {
        public static Point3D p = new Point3D(-1,-1,-1);
        private static bool running = false;
        private static Thread thread = new Thread(ThreadFunction);

        public static void Start() {
            if (!running)
            {
                running = true;
                thread.Start();
            }
        }

        public static void Stop()
        {
            running = false;
            thread.Join();
        }

        private static void ThreadFunction() {
            Connection con;
            if ((con = Connection.GetConnection(IPAddress.Parse("192.168.150.2"), 9998, 3000)) == null) 
            {
                return;
            }
            while (running) {
                p = con.GetXYZ();
            }
        }

        public static Point3D GetP() { return p; }

    }
}
