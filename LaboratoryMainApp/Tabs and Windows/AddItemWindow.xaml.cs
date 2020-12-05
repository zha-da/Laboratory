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
            MainWindow.semester.Add(new Test(
                (DateTime)DPT.SelectedDate, 
                TBT1.Text, 
                TBT2.Text, 
                int.Parse(TBT3.Text), 
                int.Parse(TBT4.Text), 
                int.Parse(TBT5.Text)));

            Close();
        }

        private void AddFPBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.semester.Add(new FailPassExam(
                (DateTime)DPFP.SelectedDate,
                TBFPD.Text,
                int.Parse(TBFPQ.Text),
                int.Parse(TBFPP.Text),
                int.Parse(TBFPM.Text)));

            Close();
        }

        private void AddControlBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.semester.Add(new Laboratory.Exams.Control(
                (DateTime)DPC.SelectedDate,
                TBCD.Text,
                int.Parse(TBCQ.Text),
                int.Parse(TBCP.Text)));

            Close();
        }

        private void AddFinalBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.semester.Add(new FinalExam(
                (DateTime)DPFE.SelectedDate,
                TBFED.Text,
                int.Parse(TBFEQ.Text),
                int.Parse(TBFEP.Text),
                int.Parse(TBFEM.Text)));

            Close();
        }
    }
}
