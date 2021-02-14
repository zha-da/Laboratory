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
using System.Windows.Shapes;
using LaboratoryMainApp;
using Laboratory.Exams;

namespace LaboratoryMainApp.Tabs_and_Windows
{
    /// <summary>
    /// Interaction logic for AddItemWindow.xaml
    /// </summary>
    public partial class AddItemWindow : Window
    {
        public AddItemWindow()
        {
            InitializeComponent();
        }

        private void AddTestBtn_Click(object sender, RoutedEventArgs e)
        {

            Close();
        }

        private void AddFPBtn_Click(object sender, RoutedEventArgs e)
        {

            Close();
        }

        private void AddControlBtn_Click(object sender, RoutedEventArgs e)
        {

            Close();
        }

        private void AddFinalBtn_Click(object sender, RoutedEventArgs e)
        {

            Close();
        }
    }
}
