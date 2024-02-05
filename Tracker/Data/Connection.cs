using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace Tracker.Data
{
    class Connection : IDisposable
    {

        public IPAddress IP;

        public int Port;

        private static readonly List<Connection> Connections = new List<Connection>();


        private Socket Socket;

        private Connection(IPAddress ip, int port, int receiveTimeout)
        {
            IP = ip;
            Port = port;

            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
            {
                ReceiveTimeout = receiveTimeout,
                
            };
            Socket.Connect(new IPEndPoint(IP, Port));

            Handshake();
        }

        private void Handshake()
        {
            byte[] receiveData = new byte[1];
            int bytesRead = Socket.Receive(receiveData);

            if (bytesRead == 1 && receiveData[0] == 23)
            {
                Socket.Send(receiveData);
            }
            else
            {
                Socket.Close();
                throw new SocketException();
            }
        }
        public static Connection GetConnection(IPAddress ip, int port, int receiveTimeout = 2000)
        {
            var conn = Connections.FirstOrDefault(c => c.IP.Equals(ip) && c.Port == port);

            if (conn is null)
            {
                try
                {
                    conn = new Connection(ip, port, receiveTimeout);
                }
                catch (SocketException) {
                    conn = null;
                }
                Connections.Add(conn);
            }

            return conn;
        }

        public Point3D GetXYZ()
        {
            var coordX = new byte[4];
            var coordY = new byte[4];
            var coordZ = new byte[4];

            Socket.Receive(coordX);
            Socket.Receive(coordY);
            Socket.Receive(coordZ);

            var x = BitConverter.ToSingle(coordX, 0);
            var y = BitConverter.ToSingle(coordY, 0);
            var z = BitConverter.ToSingle(coordZ, 0);

            var p = new Point3D(x, y, z);

            return Converter.ToAngle_FromRadian(p);
        }

        public void Dispose()
        {
            Connections.Remove(this);
            Socket.Close();
        }
    }
}
