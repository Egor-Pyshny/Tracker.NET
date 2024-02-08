using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Tracker.UserControls.Scope
{
    public class Scope
    {
        public Image image { private set; get; }
        private BitmapImage _bitmap = new BitmapImage();
        public int width = 1280;
        public int height = 720;
        public double step = 0.01;
        private float fault = 0.15f;
        public double x_center = -0.9, y_center = -1.7, max_y = 15.7, max_x = 14.6;

        public Scope() {
            image = new Image();
            _bitmap.BeginInit();
            _bitmap.UriSource = new Uri("scope.png", UriKind.RelativeOrAbsolute);
            _bitmap.EndInit();
            image.Source = _bitmap;
            image.HorizontalAlignment = HorizontalAlignment.Left;
            image.VerticalAlignment = VerticalAlignment.Top;
            image.Width = 25;
            image.Height = 25;
        }

        public Scope(double x_center, double y_center, double max_y, double max_x) { 
            
        }

        public Scope(int x, int y)
        {
            image = new Image();
            image.Source = _bitmap;
            image.HorizontalAlignment = HorizontalAlignment.Left;
            image.VerticalAlignment = VerticalAlignment.Top;
            image.Width = 25;
            image.Height = 25;
            image.Margin = new Thickness(x, y, 0, 0);
        }

        public void MoveByKeys(KeyEventArgs e) {
            Thickness currentMargin = image.Margin;
            int step = 5;
            switch (e.Key)
            {
                case Key.Left:
                    currentMargin.Left -= step;
                    break;

                case Key.Right:
                    currentMargin.Left += step;
                    break;

                case Key.Up:
                    currentMargin.Top -= step;
                    break;

                case Key.Down:
                    currentMargin.Top += step;
                    break;
            }
            if (currentMargin.Left < 0) currentMargin.Left = 0;
            if (currentMargin.Left > width - image.Width) currentMargin.Left = width - image.Width;
            if (currentMargin.Top < 0) currentMargin.Top = 0;
            if (currentMargin.Top > height - image.Height) currentMargin.Top = height - image.Height;
            image.Margin = currentMargin;
        }

        public Point MoveByAngles(double x, double y) {
            double delta_x = 0 - x;
            double delta_y = 0 - y;
            if (Math.Abs(delta_x) > fault && Math.Abs(delta_y) > fault)
            {
                double scaling_coef_x = Math.Abs(x / max_x);
                double scaling_coef_y = Math.Abs(y / max_y);
                var left = x < 0 ? step * scaling_coef_x : -step * scaling_coef_x;
                var top = y < 0 ? step * scaling_coef_y : -step * scaling_coef_y;
                return new Point(left, top);
            }
            return new Point(double.NaN, double.NaN);
        }

        public Point MoveProjection(double x, double y) {
            var new_x = x * width / max_x;
            var new_y = y * height/ max_y;
            if (new_x < 0) new_x = 0;
            if (new_x > width - 25) new_x = width - 25;
            if (new_y < 0) new_y = 0;
            if (new_y > height - 25) new_y = height - 25;
            return new Point(new_x, new_y);
        }

        public Point GetCenter() {
            Thickness currentMargin = image.Margin;
            return new Point(currentMargin.Left+12, currentMargin.Top+12);    
        }
    }
}
