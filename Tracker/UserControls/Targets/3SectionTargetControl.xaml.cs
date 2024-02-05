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

namespace Tracker.UserControls.Targets
{
    /// <summary>
    /// Логика взаимодействия для _3SectionTargetControl.xaml
    /// </summary>
    public partial class _3SectionTargetControl : UserControl, ITarget
    {
        private List<Ellipse> ellipses;
        private Point center;
        private int[] points = new int[] { 1, 2, 3, 0 };
        public Mode mode { get; set; }
        public _3SectionTargetControl()
        {
            InitializeComponent();
            ellipses = new List<Ellipse>() { el1, el2, el3 };
        }

        public int GetPoints(int x, int y, float scale)
        {
            center = new Point(this.Margin.Left + this.Width / 2, this.Margin.Top + this.Height / 2);
            int ring = (int)Math.Round(25 * scale);
            int center_rad = (int)Math.Round(50 * scale);
            int deltaX = (int)(center.X - x);
            int deltaY = (int)(center.Y - y);
            double diatance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
            int cmp = center_rad;
            int ind = 0;
            for (int i = 0; i < 3; i++)
            {
                if (diatance > cmp)
                {
                    ind++;
                }
                else break;
                cmp += ring;
            }
            //Console.WriteLine(points[ind]);
            return points[ind];
        }
    }
}
