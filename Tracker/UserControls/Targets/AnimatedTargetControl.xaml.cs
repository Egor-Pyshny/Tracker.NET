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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tracker.UserControls.Targets
{
    /// <summary>
    /// Логика взаимодействия для AnimatedTargetControl.xaml
    /// </summary>
    public partial class AnimatedTargetControl : UserControl, ITarget
    {
        public Mode mode { get; set; }
        public Point _startPoint;
        public Vector _startTransform;
        public TargetStatistic statistic = new TargetStatistic();
        private bool animating = false;
        private bool completed = false;
        private bool hited = false;
        private SolidColorBrush colorBrush = new SolidColorBrush();
        private static BackEase backEase = new BackEase() { 
            Amplitude = 0.01,
            EasingMode = EasingMode.EaseOut,
        };
        private ColorAnimation colorAnimation = new ColorAnimation(Color.FromRgb(0, 255, 0), TimeSpan.FromSeconds(1))
        {
            EasingFunction = backEase
        };

        public AnimatedTargetControl()
        {
            InitializeComponent();
            colorBrush.Color = Color.FromArgb(0xFF, 0x00, 0x00, 0x00);
        }

        public bool ScopeInTarget(int x, int y, float scale) {
            Point center = new Point(this.controlTranslateTransform.X + target.Width / 2, this.controlTranslateTransform.Y + target.Height / 2);
            int rad = (int)Math.Round(75 * scale);
            int deltaX = (int)(center.X - x);
            int deltaY = (int)(center.Y - y);
            double diatance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);            
            return diatance<rad;
        }

        public bool Check(int x, int y, float scale) {
            if (!completed)
            {
                if (ScopeInTarget(x, y, scale))
                {
                    if (!animating)
                    {
                        if (!hited) {
                            statistic.first_hit = new Point(x, y);
                            statistic.first_hit_time = DateTime.Now;
                        }
                        animating = true;
                        StartAnimation();
                    }
                }
                else
                {
                    if (animating)
                    {
                        animating = false;
                        StopAnimation();
                    }
                }
            }
            return completed;
        }

        private void OnComplete(object sender, EventArgs e) {
            target.Opacity = 0.2;
            completed = true;
        }

        private void StopAnimation()
        {
            target.Fill = Brushes.Gray;
            colorAnimation.Completed -= OnComplete;
            colorBrush.BeginAnimation(SolidColorBrush.ColorProperty, null);
        }

        private void StartAnimation()
        {
            target.Fill = colorBrush;
            colorAnimation.Completed += OnComplete;
            colorBrush.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);
        }

        public int GetPoints(int x, int y, float scale)
        {
            return 0;
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
                if (newX >= 0 && newY >= 0 && newY <= 720 - 150 && newX <= 1280 - 150)
                {
                    controlTranslateTransform.X = (_startTransform + offset).X;
                    controlTranslateTransform.Y = (_startTransform + offset).Y;
                }
            }
        }
    }
}
