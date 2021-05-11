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
using Microsoft.Win32;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Threading;
using System.Diagnostics;
using System.Text.RegularExpressions;

using System.Windows.Threading;

namespace LaboratoryMain
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            mainControl.Content = new UserControls.MenuStart(this);
        }

        private void mainControl_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        internal void ApplyNewControl(System.Windows.Controls.UserControl uc)
        {
            mainControl.Content = uc;
        }
    }
}

