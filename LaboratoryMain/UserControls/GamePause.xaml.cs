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
    /// Interaction logic for GamePause.xaml
    /// </summary>
    public partial class GamePause : UserControl
    {
        public Window ParentWindow { get; set; }
        public GamePause()
        {
            InitializeComponent();
            //cc.Focus();
            //Visibility = Visibility.Visible;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            (ParentWindow as MainWindow)?.ApplyNewControl(new MenuStart(ParentWindow));
        }

        public void SetVisible()
        {
            Visibility = Visibility.Visible;
        }
        public void SetHidden()
        {
            Visibility = Visibility.Hidden;
        }
    }
}
