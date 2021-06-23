﻿using System;
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
using System.Windows.Threading;

namespace Arcanoid
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Ellipse Ball;
        public Rectangle Platform;
        public int zone = 5;

        DispatcherTimer MyTimer;
        private int dx, dy;
        private int xBall, yBall;
        public MainWindow()
        {
            InitializeComponent();

            InitBall();
            InitPlatform();
            dx = dy = -1;
            xBall = yBall = 7;
            

            MyTimer = new DispatcherTimer();
            MyTimer.Interval = TimeSpan.FromMilliseconds(500);
            MyTimer.Tick += new EventHandler(SetBall);
            MyTimer.Start();



        }
        private void InitPlatform()
        {
            Platform = new Rectangle();
            Platform.Fill = Brushes.Blue;
            Platform.Width = 200;
            Platform.Height = 90;
            grd.Children.Add(Platform);

            Grid.SetRow(Platform, 10);
            Grid.SetColumn(Platform, 10);
            Grid.SetColumnSpan(Platform, 3);

        }

        private void InitBall()
        {
            Ball = new Ellipse();
            Ball.Fill = Brushes.Red;
            Ball.Width = 16;
            Ball.Height = 16;
            grd.Children.Add(Ball);
        }

        private void SetBall(object sender, EventArgs e)
        {
            
            Console.WriteLine(dx);
            Grid.SetRow(Ball, xBall);
            Grid.SetColumn(Ball, yBall);

            if (Grid.GetColumn(Ball) == 29 || Grid.GetColumn(Ball) == 0)
            {
                dy = -dy;
                Console.WriteLine("Test1");
                Console.WriteLine(Grid.GetColumn(Ball));

            }
            if (Grid.GetRow(Ball) == 0)
            {
                dx = -dx;
                Console.WriteLine("Test2");
            }
            

            CheckColWithPlatform();
            xBall += dx; yBall += dy;
        }

        private void CheckColWithPlatform()
        {
            if (Grid.GetRow(Ball) == zone)
            {
                if (Grid.GetColumn(Ball) <= Grid.GetColumn(Platform) + 2 &&
                    Grid.GetColumn(Ball) >= Grid.GetColumn(Platform))
                {

                }
                else
                {

                }
            }
        }
    }
}
