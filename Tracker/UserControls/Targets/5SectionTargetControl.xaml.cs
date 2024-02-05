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
using Tracker.UserControls.Targets;

namespace Tracker.UserControls.Targets
{
    /// <summary>
    /// Логика взаимодействия для _5SectionTargetControl.xaml
    /// </summary>
    public partial class _5SectionTargetControl : UserControl, ITarget
    {
        public Point _startPoint;
        public Vector _startTransform;
        public int ttl = 1;
        public Mode mode { get; set; }

        public _5SectionTargetControl()
        {
            InitializeComponent();
            grid.BorderBrush = Brushes.Black;
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (mode == Mode.DESIGN)
            {
                grid.BorderThickness = new Thickness(1, 1, 1, 1);
                // Сохраняем начальные значения до того как захватываем мышь
                _startPoint = e.GetPosition(Parent as Window);
                _startTransform = new Vector(controlTranslateTransform.X, controlTranslateTransform.Y);
                CaptureMouse();
            }
        }

        private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (mode == Mode.DESIGN)
            {
                grid.BorderThickness = new Thickness(0, 0, 0, 0);
                ReleaseMouseCapture();
            }
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMouseCaptured && mode == Mode.DESIGN)
            {
                Vector offset = Point.Subtract(e.GetPosition(Parent as Window), _startPoint);
                double newX = (_startTransform + offset).X;
                double newY = (_startTransform + offset).Y;
                if (newX >= 0 && newY >= 0 && newY <= 720 - 300 && newX <= 1280 - 300)
                {
                    controlTranslateTransform.X = (_startTransform + offset).X;
                    controlTranslateTransform.Y = (_startTransform + offset).Y;
                }
            }
        }

        private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.Name = "Control";
        }

        private void UserControl_LostFocus(object sender, RoutedEventArgs e)
        {
            this.Name = "Control1";
        }

        public int GetPoints(int x, int y, float scale)
        {
            throw new NotImplementedException();
        }
    }
}
