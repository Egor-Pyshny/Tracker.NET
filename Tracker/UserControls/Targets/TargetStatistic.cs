using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Tracker.UserControls.Targets
{
    public class TargetStatistic
    {
        public List<Point> way = new List<Point>();
        public DateTime startTime;
        public DateTime endTime;
        public TimeSpan duration;
    }
}
