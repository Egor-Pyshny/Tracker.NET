using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Tracker.UserControls.Targets
{
    public class TargetStatistic : INotifyPropertyChanged
    {
        public PointCollection way = new PointCollection();
        public DateTime startTime
        {
            get;
            set;
        }
        public DateTime first_hit_time;
        public DateTime endTime
        {
            get;
            set;
        }
        public TimeSpan duration;
        public Color lineColor;
        public Point first_hit;
        public Point last_hit;
        public int index
        {
            get;
            set;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        public void set_line_color()
        {       
            Random random = new Random(DateTime.Now.Millisecond);
            this.lineColor = Color.FromRgb((byte)random.Next(255), (byte)random.Next(255), (byte)random.Next(255));
        }

        public override string ToString()
        {
            return $"{this.startTime.TimeOfDay} - {this.endTime.TimeOfDay} - {this.index}";
        }
    }
}
