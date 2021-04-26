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
    /// Interaction logic for MenuRecords.xaml
    /// </summary>
    public partial class MenuRecords : UserControl
    {
        Window ParentWindow;
        UserControl UCParent;

        public MenuRecords()
        {
            InitializeComponent();
        }

        public MenuRecords(Window parent)
        {
            InitializeComponent();
            ParentWindow = parent;
        }

        public MenuRecords(Window parentWindow, UserControl uCParent)
        {
            InitializeComponent();
            ParentWindow = parentWindow;
            UCParent = uCParent;
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape && UCParent != null)
            {
                (ParentWindow as MainWindow).ApplyNewControl(UCParent);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.cc.Focus();
        }
    }
}
