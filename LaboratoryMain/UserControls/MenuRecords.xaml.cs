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

            Style s = Application.Current.Resources["Pixel7"] as Style;

            ApplyRecords("classic");
            ApplyRecords("endless");
            ApplyRecords("sbdestroyer");
            ApplyRecords("sbclicks");
        }
        void ApplyRecords(string tag)
        {
            List<RecordPair> records = RecordSaver.GetRecords(tag);
            Style s = Application.Current.Resources["Pixel7"] as Style;
            ListBox rs = lbClassic;
            switch (tag)
            {
                case "classic":
                    break;
                case "endless":
                    rs = lbEndless;
                    break;
                case "sbdestroyer":
                    rs = lbSbD;
                    break;
                case "sbclicks":
                    rs = lbClicks;
                    break;
            }

            if (records == null) return;

            foreach (var r in records)
            {
                rs.Items.Add(new TextBlock
                {
                    Text = $"{r.Name} - {r.Value}",
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
                    Style = s,
                    FontSize = 20
                });
            }
        }
    }
}
