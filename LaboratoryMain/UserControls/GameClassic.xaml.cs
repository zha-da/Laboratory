using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
using System.Xml.Serialization;

namespace LaboratoryMain.UserControls
{
    /// <summary>
    /// Interaction logic for GameClassic.xaml
    /// </summary>
    public partial class GameClassic : UserControl, INotifyPropertyChanged
    {
        DispatcherTimer updateTimer = new DispatcherTimer();
        DispatcherTimer bulletTimer = new DispatcherTimer();
        BackgroundWorker bgWorker = new BackgroundWorker();
        bool goright, goleft, gamepaused = false;
        double cHeight, cWidth;
        List<Rectangle> disposeOf = new List<Rectangle>();

        Window ParentWindow;
        UserControl UCParent;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyRaised(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }

        int totalEnemies;
        int enemyImg = 0;
        int totalTime = 0;
        int level = 0;

        public int enemySpeed = 5;
        public int enemyLimit = 10;
        public int playerSpeed = 3;
        public int bulletSpeed = 300;

        public GameClassic()
        {
            InitializeComponent();
        }

        public GameClassic(Window parentWindow, UserControl uCParent)
        {
            InitializeComponent();
            ParentWindow = parentWindow;
            UCParent = uCParent;
            ParentWindow.ResizeMode = ResizeMode.NoResize;

            PauseScreen.ParentWindow = ParentWindow;

            Loaded += (s, e) =>
            {
                cHeight = mainCanvas.ActualHeight;
                cWidth = mainCanvas.ActualWidth;
                StartGame();
            };
        }

        private void NewLevel()
        {
            level++;
            if (level > 5) return;

            WinScreen.Visibility = Visibility.Hidden;
            mainCanvas.Focus();

            foreach (Rectangle rectangle in disposeOf)
            {
                mainCanvas.Children.Remove(rectangle);
            }
            disposeOf.Clear();

            bgWorker.WorkerSupportsCancellation = true;
            bgWorker.CancelAsync();

            updateTimer.Start();

            bulletTimer.Interval = TimeSpan.FromMilliseconds(bulletSpeed - (level - 1) * 50);
            bulletTimer.Start();

            enemySpeed += (level - 1);
            playerSpeed += (level - 1);
            enemyLimit = level * 10 * (1 * ((int)cHeight / 400));

            CreateEnemies(enemyLimit);
        }

        private void StartGame()
        {
            ParentWindow.Focusable = false;
            mainCanvas.Focus();
            Canvas.SetLeft(player, cWidth / 2);
            Canvas.SetTop(player, cHeight - 80);

            bgWorker.DoWork += (s, e) =>
            {
                Dispatcher.Invoke(() =>
                {
                    tblvl.Text = "LEVEL " + (level + 1);
                    LevelupScreen.Visibility = Visibility.Visible;
                });
                System.Threading.Thread.Sleep(3000);
                Dispatcher.Invoke(() =>
                {
                    LevelupScreen.Visibility = Visibility.Hidden;
                });
            };
            bgWorker.RunWorkerCompleted += (s, e) => { NewLevel(); };

            ImageBrush skinBrush = new ImageBrush();
            skinBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/player.png"));
            player.Fill = skinBrush;

            updateTimer.Tick += Update;
            updateTimer.Interval = TimeSpan.FromMilliseconds(10);

            bulletTimer.Tick += CreateBullet;

            bgWorker.RunWorkerAsync();
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
            }
        }

        

        private void Update(object sender, EventArgs e)
         {
            if (gamepaused) return;

            totalTime += 10;

            Rect plr = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width, player.Height);                //создание прямоугольника с координатами игрока
            enemiesLeft.Content = "Enemies Left : " + totalEnemies;

            if (goleft && Canvas.GetLeft(player) > 15) Canvas.SetLeft(player, Canvas.GetLeft(player) - playerSpeed);                  //перемещение игрока(скорость 3)
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
                        ccgo.Focus();
                        GameOverScreen.KeyUp += (s, ew) =>
                        {
                            if (ew.Key == Key.Escape)
                            {
                                (ParentWindow as MainWindow).ApplyNewControl(new MenuStart(ParentWindow));
                            }
                            else if (ew.Key == Key.Enter)
                            {
                                (ParentWindow as MainWindow).ApplyNewControl(new GameClassic(ParentWindow, UCParent));
                            }
                        };
                    }
                }
                foreach (Rectangle rectangle in disposeOf)
                {
                    mainCanvas.Children.Remove(rectangle);
                }
                if (totalEnemies < 1)                                                                                       //победа
                {
                    goleft = goright = false;
                    updateTimer.Stop();
                    bulletTimer.Stop();

                    if (level < 5)
                    {
                        if (!bgWorker.IsBusy)
                        {
                            bgWorker.RunWorkerAsync(); 
                        }
                    }
                    else if (WinScreen.Visibility != Visibility.Visible)
                    {

                        WinScreen.Visibility = Visibility.Visible;
                        tbname.Focus();
                        TotalTimeTb.Text = "Your total time is " + (totalTime / 1000).ToString() +
                            "." + (totalTime % 1000).ToString() + " ms";
                        WinScreen.PreviewKeyUp += (s, ew) =>
                        {
                            if (ew.Key == Key.Enter)
                            {
                                if (!string.IsNullOrEmpty(tbname.Text))
                                {
                                    List<RecordPair> records = RecordSaver.GetRecords("classic");
                                    RecordSaver.SaveRecords(records, "classic", tbname.Text, totalTime);
                                }

                                (ParentWindow as MainWindow).ApplyNewControl(new GameClassic(ParentWindow, UCParent));
                            }
                            else if (ew.Key == Key.Escape)
                            {
                                if (!string.IsNullOrEmpty(tbname.Text))
                                {
                                    List<RecordPair> records = RecordSaver.GetRecords("classic");
                                    RecordSaver.SaveRecords(records, "classic", tbname.Text, totalTime);
                                }

                                (ParentWindow as MainWindow).ApplyNewControl(new MenuStart(ParentWindow));
                            }
                        };
                    }
                }
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
