using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace Tracker.Data
{
    public class Converter
    {
        public static float ToRadian_FromAngle(double angle) => (float)(angle * Math.PI / 180);

        public static double ToAngle_FromRadian(double radian) => (radian * 180 / Math.PI);

        public static Point3D ToAngle_FromRadian(Point3D radian) 
            => new Point3D ( ToAngle_FromRadian(radian.X), 
                             ToAngle_FromRadian(radian.Y), 
                             ToAngle_FromRadian(radian.Z) 
                           );
    }
}
