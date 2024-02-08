using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Threading;
using Tracker;
using Tracker.AppContext;
using Tracker.Data;
using Tracker.UserControls;
using Tracker.UserControls.Scope;
using Tracker.UserControls.Targets;
using static System.Net.Mime.MediaTypeNames;

namespace Tracker
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private int targets_amount = 5;
        private int minttl = 1;
        private int maxttl = 1;
        private double scale = 1;
        private bool runnig = false;
        private Thread thread;
        private Scope scope;
        private Timer timer, shoot;
        private UserControl target;
        private bool isgame = true;
        private int current = 0;
        private Point3D p;
        Stopwatch stopwatch = new Stopwatch();
        private List<TargetModel> targets_list = new List<TargetModel>();
        private List<AnimatedTargetControl> anumated_targets_list = new List<AnimatedTargetControl>();
        public GameWindow()
        {
            InitializeComponent();
            this.Width = Context.Game.GameAreaWidth;
            this.Height = Context.Game.GameAreaHeight + 60;
            Reciver.Start();
            scope = new Scope();
            //timer = new Timer(SwitchTarget1,new object(), Timeout.Infinite, Timeout.Infinite);
            //shoot = new Timer(CalculateScore, new object(), 20, 20);
            game_grid.Children.Add(scope.image);
            Grid.SetZIndex(scope.image, 1);
            //GenerateTargets();
            MyWindowController.register(this);
            runnig = true;
            thread = new Thread(ThreadFunc);
            //thread.Start();
        }

        private void ThreadFunc()
        {
            while (runnig) {
                p = Reciver.p;
                p.X -= scope.x_center;
                p.Y -= scope.y_center;
                Point temp = scope.MoveProjection(p.X, p.Y);
                Dispatcher.Invoke(() =>
                {
                    animtarget.Check((int)temp.X + 12, (int)temp.Y + 12, 1);
                    Thickness currentMargin = scope.image.Margin;
                    currentMargin.Left = temp.X;
                    currentMargin.Top = temp.Y;
                    scope.image.Margin = currentMargin;
                });
            }
        }

        public void Stop() {
            runnig = false;
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
            MyWindowController.Close(this);
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
            Dispatcher.Invoke(() =>
            {
                animtarget.Check((int)scope.image.Margin.Left + 12, (int)scope.image.Margin.Top + 12, 1);
            });
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
