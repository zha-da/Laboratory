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

namespace LaboratoryMain.UserControls
{
    /// <summary>
    /// Interaction logic for MenuStart.xaml
    /// </summary>
    public partial class MenuStart : UserControl
    {
        Window ParentWindow;
        public MenuStart()
        {
            InitializeComponent();
        }
        public MenuStart(Window parent)
        {
            InitializeComponent();
            ParentWindow = parent;
        }

        private void menuButton_Click(object sender, RoutedEventArgs e)
        {
            //var mainWindow = Window.GetWindow(this);
            switch (((sender as Button).Content as TextBlock).Text)
            {
                case "Exit":
                    ParentWindow.Close();
                    break;
                case "Records":
                    (ParentWindow as MainWindow).ApplyNewControl(new MenuRecords(ParentWindow, this));
                    break;
                case "Classic mode":
                    (ParentWindow as MainWindow).ApplyNewControl(new GameClassic(ParentWindow, this));
                    break;
                case "SpaceBar Destroyer":
                    (ParentWindow as MainWindow).ApplyNewControl(new GameSpacebarDestroyer(ParentWindow, this));
                    break;
                case "Endless mode":
                    (ParentWindow as MainWindow).ApplyNewControl(new GameEndless(ParentWindow, this));
                    break;
                default:
                    (ParentWindow as MainWindow).ApplyNewControl(new MenuRecords(ParentWindow, this));
                    break;
            }
        }
    }
}
