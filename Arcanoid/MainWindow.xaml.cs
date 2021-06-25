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
        public int zone = 28;

        DispatcherTimer MyTimer;
        private int dx, dy;
        private int xBall, yBall;
        private List<Rectangle> Bricks;
        public MainWindow()
        {
            InitializeComponent();

            dx = dy = 1;
            xBall = 15;
            yBall = 24;
            Bricks = new List<Rectangle>();

            InitBall();
            InitPlatform();
            GenerateBricks();
            
            MyTimer = new DispatcherTimer();
            MyTimer.Interval = TimeSpan.FromMilliseconds(500);
            MyTimer.Tick += new EventHandler(SetBall);
            MyTimer.Start();

        }

        private void GenerateBricks()
        {
            for (int i = 2; i < 10; i += 2)
            {
                for (int j = 5; j < 30; j += 5)
                {
                    Rectangle Brick = new Rectangle();
                    Brick.Fill = Brushes.Yellow;
                    Brick.Width = 50;
                    Brick.Height = 50;
                    grd.Children.Add(Brick);

                    Grid.SetRow(Brick, i);
                    Grid.SetColumn(Brick, j);
                    Bricks.Add(Brick);

                }
            }
        }

        private void InitPlatform()
        {
            Platform = new Rectangle();
            Platform.Fill = Brushes.Blue;
            Platform.Width = 200;
            Platform.Height = 90;
            grd.Children.Add(Platform);

            Grid.SetRow(Platform, 29);
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

        private void TurnRight()
        {
            int now_pos = Grid.GetColumn(Platform);
            if (now_pos < 27)
            {
                Grid.SetColumn(Platform, now_pos + 1);
            }
            
        }

        private void TurnLeft()
        {
            int now_pos = Grid.GetColumn(Platform);
            if (now_pos > 0)
            {
                Grid.SetColumn(Platform, now_pos - 1);
            }
        }

        private void Window_KeyDown_1(object sender, KeyEventArgs e)
        {
            
            switch (e.Key)
            {
                case Key.Left:
               
                    TurnLeft();
                    break;
                case Key.Right:
                    
                    TurnRight();
                    break;
            }
        }

        private void SetBall(object sender, EventArgs e)
        {
            Grid.SetRow(Ball, xBall);
            Grid.SetColumn(Ball, yBall);
            CheckBrick();
            if (Bricks.Count == 0)
            {
                MessageBox.Show("You win!");
            }
            CheckColWithPlatform();
            CheckColWithWall();
            xBall += dx; yBall += dy;
        }

        private void CheckBrick()
        {
            for (int i = 0; i < Bricks.Count; ++i)
            {
                if (Grid.GetColumn(Bricks[i]) == yBall &&
                    Grid.GetRow(Bricks[i]) == xBall + 1)
                {
                    
                    dx = -dx;
                    grd.Children.Remove(Bricks[i]);
                    Bricks.Remove(Bricks[i]);
                    break;
                }
                else if (Grid.GetColumn(Bricks[i]) == yBall - 1 &&
                    Grid.GetRow(Bricks[i]) == xBall)
                {
                    
                    dy = -dy;
                    grd.Children.Remove(Bricks[i]);
                    Bricks.Remove(Bricks[i]);
                    break;
                }
                else if (Grid.GetColumn(Bricks[i]) == yBall &&
                    Grid.GetRow(Bricks[i]) == xBall - 1)
                {
                    
                    dx = -dx;
                    grd.Children.Remove(Bricks[i]);
                    Bricks.Remove(Bricks[i]);
                    break;
                }
                else if (Grid.GetColumn(Bricks[i]) == yBall + 1 &&
                    Grid.GetRow(Bricks[i]) == xBall)
                {
                    
                    dy = -dy;
                    grd.Children.Remove(Bricks[i]);
                    Bricks.Remove(Bricks[i]);
                    break;
                }
                else if (Grid.GetColumn(Bricks[i]) == yBall + dy &&
                    Grid.GetRow(Bricks[i]) == xBall + dx)
                {

                    dy = -dy;
                    dx = -dx;
                    grd.Children.Remove(Bricks[i]);
                    Bricks.Remove(Bricks[i]);
                    break;
                }
            }
        }

        private void CheckColWithWall()
        {
            if (Grid.GetRow(Ball) == 0) dx = -dx;
            if (Grid.GetColumn(Ball) == 0 || Grid.GetColumn(Ball) == 29) dy = -dy;
        }

        private void CheckColWithPlatform()
        {
            if (Grid.GetRow(Ball) == zone)
            {
                if (Grid.GetColumn(Ball) <= Grid.GetColumn(Platform) + 2 &&
                    Grid.GetColumn(Ball) >= Grid.GetColumn(Platform))
                {
                    dx = -dx;
                    
                }
                else if (Grid.GetColumn(Ball) + dy <= Grid.GetColumn(Platform) + 2 &&
                    Grid.GetColumn(Ball) >= Grid.GetColumn(Platform))
                {
                    dx = -dx;
                    dy = -dy;
                }
                else
                {
                    MessageBox.Show("You lose!");
                }
            }
        }


    }
}
