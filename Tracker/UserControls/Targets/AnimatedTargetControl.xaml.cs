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
        private bool animating = false;
        private bool completed = false;
        private SolidColorBrush colorBrush = new SolidColorBrush();
        private BackEase backEase = new BackEase() { 
            Amplitude = 0.1,
            EasingMode = EasingMode.EaseOut,
        };

        public AnimatedTargetControl()
        {
            InitializeComponent();
            colorBrush.Color = Color.FromArgb(0xFF, 0x80, 0x80, 0x80);
        }

        public bool ScopeInTarget(int x, int y, float scale) {
            Point center = new Point(this.Margin.Left + 250 / 2, this.Margin.Top + 250 / 2);
            int rad = (int)Math.Round(125 * scale);
            int deltaX = (int)(center.X - x);
            int deltaY = (int)(center.Y - y);
            double diatance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);            
            return diatance<rad;
        }

        public void Check(int x, int y, float scale) {
            if (!completed)
            {
                if (ScopeInTarget(x, y, scale))
                {
                    if (!animating)
                    {
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
        }

        private void OnComplete(object sender, EventArgs e) { 
            target.Opacity = 0.2;
            completed = true;
        }

        private void StopAnimation()
        {
            target.Fill = Brushes.Gray;
            colorBrush.BeginAnimation(SolidColorBrush.ColorProperty, null);
        }

        private void StartAnimation()
        {
            target.Fill = colorBrush;
            ColorAnimation colorAnimation = new ColorAnimation(Color.FromRgb(0, 255, 0), TimeSpan.FromSeconds(3))
            {
                EasingFunction = backEase
            };
            colorAnimation.Completed += OnComplete;
            colorBrush.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);
        }

        public int GetPoints(int x, int y, float scale)
        {
            return 0;
        }
    }
}
