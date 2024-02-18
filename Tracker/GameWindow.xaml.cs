using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using System.Windows.Threading;
using Tracker;
using Tracker.AppContext;
using Tracker.Data;
using Tracker.UserControls;
using Tracker.UserControls.Scope;
using Tracker.UserControls.Targets;

namespace Tracker
{

    public class TimeOnlyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dateTime)
            {
                return dateTime.ToString("HH:mm:ss");
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private int targets_amount = 5;
        private int minttl = 1;
        private int maxttl = 1;
        private int ind = 0;
        private int stat_ind = 0;
        private double scale = 1;
        private bool runnig = false;
        private Thread thread;
        private Scope scope;
        private Timer timer, path_logger;
        private UserControl target;
        private bool isgame = true;
        private int current = 0;
        private Point3D p;
        Stopwatch stopwatch = new Stopwatch();
        private List<TargetModel> targets_list = new List<TargetModel>();
        private AnimatedTargetControl currentTarget;
        private List<AnimatedTargetControl> animated_targets_list = new List<AnimatedTargetControl>();
        private TargetStatistic stat = new TargetStatistic();

        public GameWindow()
        {
            InitializeComponent();
            Reciver.Start();

            Start();
            this.DataContext = stat;
            path_logger = new Timer(log_path, null, 0, 16);
            MyWindowController.register(this);          
            runnig = true;
        }

        private void ThreadFunc()
        {
            while (runnig) {
                p = Reciver.p;
                p.X -= scope.x_center;
                p.Y -= scope.y_center;
                Point temp = scope.MoveProjection(p.X, p.Y);            
                bool completed = false;
                Dispatcher.Invoke(() =>
                {
                    var t = scope.image.Margin;
                    completed = currentTarget.Check((int)t.Left + 12, (int)t.Top + 12, 1);
                    if(completed) currentTarget.statistic.last_hit = new Point((int)t.Left + 12, (int)t.Top + 12);
                    Thickness currentMargin = scope.image.Margin;
                    currentMargin.Left = temp.X;
                    currentMargin.Top = temp.Y;
                    //scope.image.Margin = currentMargin;
                });
                if (completed)
                {
                    SwitchTarget();
                }
            }
        }

        private void log_path(object state)
        {
            if (ind != 1 && isgame)
            {
                Dispatcher.Invoke(() =>
                {
                    var t = scope.image.Margin;
                    currentTarget.statistic.way.Add(new Point(t.Left+12, t.Top+12));
                });
            }
        } 

        private void SwitchTarget() {
            if (isgame)
            {
                if (ind < animated_targets_list.Count)
                {
                    currentTarget.statistic.index = ind;
                    currentTarget.statistic.set_line_color();
                    currentTarget.statistic.endTime = DateTime.Now;
                    currentTarget = animated_targets_list[ind];
                    Dispatcher.Invoke(() =>
                    {
                        currentTarget.Visibility = Visibility.Visible;
                    });
                    currentTarget.statistic.startTime = DateTime.Now;
                    ind++;
                }
                else
                {
                    currentTarget.statistic.set_line_color();
                    isgame = false;
                    Dispatcher.Invoke(() =>
                    {
                        ButtonOpenMenu.IsEnabled = true;
                        DrawPathes();
                    });
                    path_logger.Dispose();
                }
            }
        }

        private void DrawPathes() {
            foreach (AnimatedTargetControl target in animated_targets_list) {
                var myPolyline = new Polyline();
                myPolyline.Stroke = new SolidColorBrush(target.statistic.lineColor);
                myPolyline.StrokeThickness = 2;
                myPolyline.FillRule = FillRule.EvenOdd;
                myPolyline.Points = target.statistic.way;
                game_grid.Children.Add(myPolyline);
            }
        }

        public void Start() {
            ButtonOpenMenu.IsEnabled = false;
            scope = new Scope(
                Context.Game.XCenterAngle,
                Context.Game.YCenterAngle,
                Context.Game.XAngle,
                Context.Game.YAngle,
                Context.Game.GameAreaWidth,
                Context.Game.GameAreaHeight
                );
            game_grid.Children.Add(scope.image);
            Grid.SetZIndex(scope.image, 1);
            animated_targets_list.Add(a1);
            //animated_targets_list.Add(a2);
            //animated_targets_list.Add(a3);
            //animated_targets_list.Add(a4);
            a5.Visibility = Visibility.Hidden;
            a6.Visibility = Visibility.Hidden;
            animated_targets_list.Add(a5);
            animated_targets_list.Add(a6);
            currentTarget = animated_targets_list[ind];
            currentTarget.statistic.index = ind;
            ind++;
            currentTarget.statistic.startTime = DateTime.Now;
            this.Width = Context.Game.GameAreaWidth;
            this.Height = Context.Game.GameAreaHeight + 60;
            runnig = true;
            thread = new Thread(ThreadFunc);
            thread.Start();
        }

        public void Stop() {
            runnig = false;
            path_logger.Dispose();
            //timer.Dispose();
            thread.Abort();
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            /*section.GetPoints((int)e.GetPosition(game_grid).X, 
                (int)e.GetPosition(game_grid).Y,
                300f/200f);*/
        }

        private void CalculateScore(object state)
        {
            //(target as ITarget).GetPoints();
        }

        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void BtnMinimize_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Button).Background = new SolidColorBrush(Color.FromRgb(55, 55, 55));
        }

        private void BtnMinimize_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Button).Background = null;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Stop();
            this.Hide();
            MyWindowController.main().Show();
            //MyWindowController.Close(this);
        }

        private void BtnClose_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Button).Background = new SolidColorBrush(Color.FromRgb(255, 0, 0));
        }

        private void BtnClose_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Button).Background = null;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void PlaceTargetsRandom() {
            Random rand = new Random();
            int prev_x = 0, prev_y = 0, prev_size = 0;
            for (int i = 0; i < targets_amount; i++)
            {
                TargetModel newtarget = targets_list[i];
                newtarget.x = rand.Next(0, (int)(game_grid.Width - newtarget.size + 1));
                newtarget.y = rand.Next(0, (int)(game_grid.Height - newtarget.size + 1));
                if (i == 0)
                {
                    prev_x = newtarget.x;
                    prev_y = newtarget.y;
                    prev_size = newtarget.size;
                    continue;
                }
                /*while (true)
                {
                    if ((newtarget.x >= prev_x - newtarget.size) && 
                        (newtarget.x <= prev_x + prev_size + newtarget.size) &&
                        (newtarget.y >= prev_y - newtarget.size) && 
                        (newtarget.y <= prev_y + prev_size + newtarget.size))
                    {
                        newtarget.x = rand.Next(0, (int)(game_grid.Width - newtarget.size + 1));
                        newtarget.y = rand.Next(0, (int)(game_grid.Height - newtarget.size + 1));
                    }
                    else break;
                }*/
            }
        }

        private void SwitchTarget1(object sender)
        {
            Dispatcher.Invoke(() =>
            {
                if (!(current == targets_list.Count))
                {
                    stopwatch.Stop();
                    Console.WriteLine(stopwatch.ElapsedMilliseconds);
                    Console.WriteLine(current);
                    target = targets_list[current].Produce((float)(game_grid.Width/1280f));
                    target.Margin = new Thickness(
                        targets_list[current].x,
                        targets_list[current].y,
                        0,
                        0
                        );
                    timer.Change(targets_list[current].ttl * 1000, Timeout.Infinite);
                    current++;
                    (target as ITarget).mode = Mode.GAME;
                    if(game_grid.Children.Count>1) game_grid.Children.RemoveAt(1);
                    game_grid.Children.Add(target);
                    stopwatch.Start();
                }
                else
                {
                    stopwatch.Stop();
                    Console.WriteLine(stopwatch.ElapsedMilliseconds);
                    Console.WriteLine("end");
                    game_grid.Children.RemoveAt(1);
                    stopwatch.Start();
                    isgame = false;
                }
            });
        }

        private void GenerateTargets()
        {
            Random rand = new Random();
            for (int i = 0; i < targets_amount; i++)
            {
                TargetModel newtarget = new TargetModel(rand.Next(3, 6));
                newtarget.ttl = rand.Next(minttl, maxttl + 1);
                targets_list.Add(newtarget);
            }
            PlaceTargetsRandom();
            timer.Change(0, Timeout.Infinite);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            scope.MoveByKeys(e);
            /*Dispatcher.Invoke(() =>
            {
               // currentTarget.Check((int)scope.image.Margin.Left + 12, (int)scope.image.Margin.Top + 12, 1);
            });*/
        }

        private void ButtonPrevTarget_Click(object sender, RoutedEventArgs e)
        {
            stat_ind--;
            ButtonNextTarget.IsEnabled = true;
            this.DataContext = animated_targets_list[stat_ind].statistic;
            target_line_color.Fill = new SolidColorBrush(animated_targets_list[stat_ind].statistic.lineColor);
            if (stat_ind == 0)
            {
                (sender as Button).IsEnabled = false;
            }
        }

        private void ButtonNextTarget_Click(object sender, RoutedEventArgs e)
        {
            stat_ind++;
            ButtonPrevTarget.IsEnabled = true;
            this.DataContext = animated_targets_list[stat_ind].statistic;
            target_line_color.Fill = new SolidColorBrush(animated_targets_list[stat_ind].statistic.lineColor);
            Console.WriteLine(stat.ToString());
            if (stat_ind == animated_targets_list.Count - 1) { 
                (sender as Button).IsEnabled = false;
            }
        }

        public void OnGameStart()
        {
            MainWindow mainWindow = (MainWindow)MyWindowController.main();
            var control = (MainPageControl)mainWindow.controls["ItemPlay"];
            minttl = (int)control.minttl_slider.Value;
            maxttl = (int)control.maxttl_slider.Value;
            targets_amount = (int)control.targets_slider.Value;
            scale = this.Width / 1280;
            GenerateTargets();
        }
    }
}
