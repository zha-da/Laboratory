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

namespace LaboratoryMain.UserControls
{
    /// <summary>
    /// Interaction logic for GameSpacebarDestroyer.xaml
    /// </summary>
    public partial class GameSpacebarDestroyer : UserControl
    {
        #region Fields
        bool goleft, goright = false;
        List<Rectangle> disposeOf = new List<Rectangle>();
        int enemyImg = 0;
        int bulletTimer;
        int bulletTimerLimit = 90;
        int totalEnemies;
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        ImageBrush skinBrush = new ImageBrush();
        int enemySpeed = 6;
        #endregion
        public GameSpacebarDestroyer()
        {
            InitializeComponent();
            Loaded += (s, e) => mainCanvas.Focus();

            dispatcherTimer.Tick += Update;
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(20);
            dispatcherTimer.Start();

            skinBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/player.png"));
            player.Fill = skinBrush;

            CreateEnemies(300);
        }

        private void mainCanvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left) goleft = true;
            else if (e.Key == Key.Right) goright = true;
        }

        private void mainCanvas_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left) goleft = false;

            if (e.Key == Key.Right) goright = false;

            if (e.Key == Key.Space)
            {
                disposeOf.Clear();

                Rectangle newbullet = new Rectangle
                {
                    Tag = "bullet",
                    Height = 20,
                    Width = 5,
                    Fill = Brushes.White,
                    Stroke = Brushes.Red
                };

                Canvas.SetTop(newbullet, Canvas.GetTop(player) - newbullet.Height);
                Canvas.SetLeft(newbullet, Canvas.GetLeft(player) + player.Width / 2);

                mainCanvas.Children.Add(newbullet);
            }
        }

        private void CreateEnemyBullets(double x, double y)
        {
            Rectangle newenemybullet = new Rectangle
            {
                Tag = "enemyBullet",
                Height = 40,
                Width = 15,
                Fill = Brushes.Yellow,
                Stroke = Brushes.Black,
                StrokeThickness = 5
            };

            Canvas.SetTop(newenemybullet, y);
            Canvas.SetLeft(newenemybullet, x);

            mainCanvas.Children.Add(newenemybullet);
        }

        private void CreateEnemies(int limit)
        {
            int left = 0;
            totalEnemies = limit;

            for (int i = 0; i < limit; i++)
            {
                ImageBrush enemySkin = new ImageBrush();

                Rectangle enemy = new Rectangle
                {
                    Tag = "enemy",
                    Height = 45,
                    Width = 45,
                    Fill = enemySkin
                };

                Canvas.SetTop(enemy, 10);
                Canvas.SetLeft(enemy, left);

                mainCanvas.Children.Add(enemy);
                left -= 60;

                enemyImg++;

                if (enemyImg > 8) enemyImg = 1;

                enemySkin.ImageSource = new BitmapImage(new Uri($"pack://application:,,,/images/invader{enemyImg}.gif"));

                //switch (enemyImg)
                //{
                //    case 1:
                //        enemySkin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/invader1.gif"));
                //        break;
                //    case 2:
                //        enemySkin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/invader2.gif"));
                //        break;
                //    case 3:
                //        enemySkin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/invader3.gif"));
                //        break;
                //    case 4:
                //        enemySkin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/invader4.gif"));
                //        break;
                //    case 5:
                //        enemySkin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/invader5.gif"));
                //        break;
                //    case 6:
                //        enemySkin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/invader6.gif"));
                //        break;
                //    case 7:
                //        enemySkin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/invader7.gif"));
                //        break;
                //    case 8:
                //        enemySkin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/invader8.gif"));
                //        break;
                //}
            }
        }

        private void Update(object sender, EventArgs e)
        {
            #region mine
            mainCanvas.Focus();
            Rect plr = new Rect(Canvas.GetLeft(player), Canvas.GetLeft(player), player.Width, player.Height);
            enemiesLeft.Content = "Enemies Left : " + totalEnemies;

            if (goleft && Canvas.GetLeft(player) > 0) Canvas.SetLeft(player, Canvas.GetLeft(player) - 10);
            else if (goright && Canvas.GetLeft(player) + 80 < System.Windows.Application.Current.MainWindow.Width) Canvas.SetLeft(player, Canvas.GetLeft(player) + 10);

            bulletTimer -= 3;

            if (bulletTimer < 0)
            {
                CreateEnemyBullets(Canvas.GetLeft(player) + 20, 10);
                bulletTimer = bulletTimerLimit;
            }

            if (totalEnemies < 10) enemySpeed = 10;

            foreach (var x in mainCanvas.Children.OfType<Rectangle>())
            {
                if (x is Rectangle && (string)x.Tag == "bullet")
                {
                    Canvas.SetTop(x, Canvas.GetTop(x) - 20);
                    Rect bullet = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if (Canvas.GetTop(x) < 10)
                    {
                        disposeOf.Add(x);
                    }

                    foreach (var y in mainCanvas.Children.OfType<Rectangle>())
                    {
                        if (y is Rectangle && (string)y.Tag == "enemy")
                        {
                            Rect enemy = new Rect(Canvas.GetLeft(y), Canvas.GetTop(y), y.Width, y.Height);

                            if (bullet.IntersectsWith(enemy))
                            {
                                disposeOf.Add(x); disposeOf.Add(y);
                                totalEnemies -= 1;
                            }
                        }
                    }
                }

                if (x is Rectangle && (string)x.Tag == "enemy")
                {
                    Canvas.SetLeft(x, Canvas.GetLeft(x) + enemySpeed);

                    if (Canvas.GetLeft(x) > 820)
                    {
                        Canvas.SetLeft(x, -80);
                        Canvas.SetTop(x, Canvas.GetTop(x) + (x.Height + 10));
                    }

                    Rect enemy = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if (plr.IntersectsWith(enemy))
                    {
                        dispatcherTimer.Stop();
                        System.Windows.Forms.MessageBox.Show("you lost");
                    }
                }

                if (x is Rectangle && (string)x.Tag == "enemyBullet")
                {
                    Canvas.SetTop(x, Canvas.GetTop(x) + 10);
                    if (Canvas.GetTop(x) > 480) disposeOf.Add(x);


                    Rect ebullet = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if (plr.IntersectsWith(ebullet))
                    {
                        dispatcherTimer.Stop();
                        System.Windows.Forms.MessageBox.Show("you lost");
                    }
                }
            }
            foreach (Rectangle rectangle in disposeOf)
            {
                mainCanvas.Children.Remove(rectangle);
            }
            if (totalEnemies < 1)
            {
                dispatcherTimer.Stop();
                System.Windows.Forms.MessageBox.Show("you won");
            }
            #endregion
        }
    }
}
