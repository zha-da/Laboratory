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

namespace LaboratoryMain
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string path;
        bool needssaving = false;
        byte needsvisibility = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void tbword_TextChanged(object sender, TextChangedEventArgs e)
        {
            string line = tbword.Text;

            if (String.IsNullOrEmpty(line)) return;

            Regex rg = new Regex(@line);
            MatchCollection mc = rg.Matches(rtbFile.Text);
            
            foreach (Match m in mc)
            {
                rtbFile.Select(m.Index, m.Length);
                rtbFile.Focus();
            }
        }
        private async void bchoose_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                rtbFile.Clear();
                path = ofd.FileName;

                //string[] fulltext = (new StreamReader(path)).ReadToEnd().Split('\n');

                await Task.Run(() =>
                {
                    using (StreamReader sr = File.OpenText(path))
                    {
                        int lines = 0;
                        string nl;
                        while ((nl = sr.ReadLine()) != null)
                        {
                            Dispatcher.BeginInvoke(new ThreadStart(delegate { rtbFile.AppendText(nl + "\n"); }));
                            lines++;
                            Thread.Sleep(3);
                        }
                        Dispatcher.BeginInvoke(new ThreadStart(delegate { tbLines.Text = "Количество строк : " + lines; }));
                    }
                });
                Title = path;
                GC.Collect();
            }
        }
        void AddLine(string line) => rtbFile.AppendText(line);

        private void rtbFile_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!needssaving || Title.Equals("MainWindow")) return;

            //using (StreamWriter sw = new StreamWriter(Title))
            //{
            //    sw.
            //}
        }

        private void bfind_Click(object sender, RoutedEventArgs e)
        {
            needsvisibility++;
            needsvisibility %= 2;

            if (needsvisibility == 1)
            {
                tbword.Visibility = Visibility.Visible;
                tbword.Focus();
            }
            else
            {
                tbword.Visibility = Visibility.Collapsed;
                tbword.Text = "";
            }
        }
    }
}
