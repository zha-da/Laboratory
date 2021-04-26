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
    /// Interaction logic for GameEndless.xaml
    /// </summary>
    public partial class GameEndless : UserControl
    {
        DispatcherTimer updateTimer = new DispatcherTimer();
        DispatcherTimer bulletTimer = new DispatcherTimer();
        bool goright, goleft, gamepaused = false;
        double cHeight, cWidth;
        List<Rectangle> disposeOf = new List<Rectangle>();

        Window ParentWindow;
        UserControl UCParent;

        int totalEnemies;
        int enemyImg = 0;
        int left = 0;
        int totalEnemiesDestroyed = 0;

        public int enemySpeed = 12;
        public int enemyLimit = 50;
        public int playerSpeed = 7;
        public int bulletSpeed = 100;

        public GameEndless()
        {
            InitializeComponent();
        }

        public GameEndless(Window parentWindow, UserControl uCParent)
        {
            InitializeComponent();
            ParentWindow = parentWindow;
            UCParent = uCParent;
            ParentWindow.ResizeMode = ResizeMode.NoResize;

            Loaded += (s, e) =>
            {
                cHeight = mainCanvas.ActualHeight;
                cWidth = mainCanvas.ActualWidth;
                StartGame();
            };
        }

        private void StartGame()
        {
            ParentWindow.Focusable = false;
            mainCanvas.Focus();
            Canvas.SetLeft(player, cWidth / 2);
            Canvas.SetTop(player, cHeight - 80);

            ImageBrush skinBrush = new ImageBrush();
            skinBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/player.png"));
            player.Fill = skinBrush;

            updateTimer.Tick += Update;
            updateTimer.Interval = TimeSpan.FromMilliseconds(10);
            updateTimer.Start();

            bulletTimer.Tick += CreateBullet;
            bulletTimer.Interval = TimeSpan.FromMilliseconds(bulletSpeed);
            bulletTimer.Start();

            CreateEnemies(2 * enemyLimit);
        }

        private void CreateBullet(object sender, EventArgs e)
        {
            if (!Keyboard.IsKeyDown(Key.Space)) return;
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
        private void CreateEnemies(int limit)
        {
            totalEnemies += limit;

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
            }
        }

        private void Update(object sender, EventArgs e)
        {
            if (gamepaused) return;

            left += enemySpeed;
            if (left > cWidth) left = -150;

            Rect plr = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width, player.Height);                //создание прямоугольника с координатами игрока
            enemiesLeft.Content = "Enemies Left : infinity";

            if (goleft && Canvas.GetLeft(player) > 15) Canvas.SetLeft(player, Canvas.GetLeft(player) - playerSpeed);                  //перемещение игрока
            else if (goright && Canvas.GetLeft(player) + 80 < cWidth)
                Canvas.SetLeft(player, Canvas.GetLeft(player) + playerSpeed);

            var ch = mainCanvas.Children.OfType<Rectangle>().ToList();
            foreach (var x in ch)
            {
                if (x is Rectangle && (string)x.Tag == "bullet")
                {
                    Canvas.SetTop(x, Canvas.GetTop(x) - 15);
                    Rect bullet = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if (Canvas.GetTop(x) < 10)                                                                              //удаление пули, дошедшей до верха окна
                    {
                        disposeOf.Add(x);
                    }

                    foreach (var y in mainCanvas.Children.OfType<Rectangle>())
                    {
                        if (y is Rectangle && (string)y.Tag == "enemy")
                        {
                            Rect enemy = new Rect(Canvas.GetLeft(y), Canvas.GetTop(y), y.Width, y.Height);                  //проверка на коллизию с врагом

                            if (bullet.IntersectsWith(enemy))
                            {
                                totalEnemiesDestroyed++;
                                disposeOf.Add(x); disposeOf.Add(y);
                                totalEnemies -= 1;
                            }
                        }
                    }
                }
                if (x is Rectangle && (string)x.Tag == "enemy")
                {
                    Canvas.SetLeft(x, Canvas.GetLeft(x) + enemySpeed);
                    
                    if (Canvas.GetLeft(x) > cWidth)                                                                         //перемещение врага, дошедшего до края карты
                    {
                        Canvas.SetLeft(x, -80);
                        Canvas.SetTop(x, Canvas.GetTop(x) + (x.Height + 10));
                    }

                    Rect enemy = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);                          //проверка на коллизию с игроком

                    if (plr.IntersectsWith(enemy))
                    {
                        goleft = goright = false;
                        updateTimer.Stop();
                        bulletTimer.Stop();
                        GameOverScreen.Visibility = Visibility.Visible;
                        tbgo.Text = "You destroyed " + totalEnemiesDestroyed + " enemies";
                        ccgo.Focus();
                        GameOverScreen.KeyUp += (s, ew) =>
                        {
                            if (ew.Key == Key.Escape)
                            {
                                (ParentWindow as MainWindow).ApplyNewControl(new MenuStart(ParentWindow));
                            }
                            else if (ew.Key == Key.Enter)
                            {
                                (ParentWindow as MainWindow).ApplyNewControl(new GameEndless(ParentWindow, UCParent));
                            }
                        };
                    }
                }
                foreach (Rectangle rectangle in disposeOf)
                {
                    mainCanvas.Children.Remove(rectangle);
                }
                if (totalEnemies < ((enemyLimit * (cHeight / 400)) + 50))
                {
                    CreateEnemies(enemyLimit * 2);
                }
                //if (totalEnemies < 1)                                                                                       //победа
                //{
                //    goleft = goright = false;
                //    updateTimer.Stop();
                //    bulletTimer.Stop();
                //    WinScreen.Visibility = Visibility.Visible;
                //    ccwin.Focus();
                //    TotalTimeTb.Text = "Your total time is " + (totalTime / 1000).ToString() +
                //        "." + (totalTime % 1000).ToString() + " ms";
                //    WinScreen.KeyUp += (s, ew) =>
                //    {
                //        if (ew.Key == Key.Enter)
                //        {
                //            Leveler.Invoke();
                //        }
                //        else if (ew.Key == Key.Escape)
                //        {
                //            (ParentWindow as MainWindow).ApplyNewControl(new MenuStart(ParentWindow));
                //        }
                //    };
                //}
            }
        }

        private void mainCanvas_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left) goleft = false;

            if (e.Key == Key.Right) goright = false;
        }

        private void mainCanvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (WinScreen.Visibility == Visibility.Visible)
            {
                ccwin.Focus();
                return;
            }
            if (GameOverScreen.Visibility == Visibility.Visible)
            {
                ccgo.Focus();
                return;
            }

            if (e.Key == Key.Left) goleft = true;
            else if (e.Key == Key.Right) goright = true;
            else if (e.Key == Key.Escape)
            {
                if (gamepaused)
                {
                    gamepaused = false;
                    PauseScreen.Visibility = Visibility.Hidden;
                }
                else if (updateTimer.IsEnabled)
                {
                    gamepaused = true;
                    PauseScreen.Visibility = Visibility.Visible;
                }
            }
        }
        public void GetFocus()
        {
            mainCanvas.Focus();
        }
    }
}
