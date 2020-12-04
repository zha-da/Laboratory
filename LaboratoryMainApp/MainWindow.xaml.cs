using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Laboratory;
using Laboratory.Exams;
using Laboratory.Exams.Comparers;
using Laboratory.AdditionalClasses;
using Microsoft.Win32;
using System.Collections;

namespace LaboratoryMainApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        HashSet<string> openedFiles = new HashSet<string>(); //хранит имена уже открытых файлов
        Semester semester = new Semester(new List<Exam>());

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            FindButton.IsEnabled = true;
            RemoveButton.IsEnabled = true;
            LoadButton.IsEnabled = false;
            UnloadButton.IsEnabled = true;
            AddNewButton.IsEnabled = true;

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            ExamsGrid.Visibility = Visibility.Visible;
            string filename = ofd.FileName;

            openedFiles.Add(filename);
            semester.Add(Semester.GetFromFile(filename));
            foreach (var item in semester.Exams)
            {
                ExamsGrid.Items.Add(item);
            }
            //ExamsGrid.ItemsSource = semester.Exams;
        }

        private void UnloadButton_Click(object sender, RoutedEventArgs e)
        {
            ExamsGrid.Visibility = Visibility.Hidden;

            ExamsGrid.Items.Clear();
            semester.Exams.Clear();
            openedFiles.Clear();

            AddNewButton.IsEnabled = false;
            UnloadButton.IsEnabled = false;
            LoadButton.IsEnabled = true;
            FindButton.IsEnabled = false;
            RemoveButton.IsEnabled = false;
        }

        private void TakeExamButton_Click(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;
            int index = int.Parse(item.Tag.ToString());
            semester.Exams[index].TakeExam();
        }

        private void AddNewButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            string filename = ofd.FileName;
            if (!openedFiles.Contains(filename))
            {
                openedFiles.Add(filename);
                var read = Semester.GetFromFile(filename);
                semester.Add(read);
                foreach (var item in read)
                {
                    ExamsGrid.Items.Add(item);
                }
                //ResetGrid();
            }
            else
            {
                MessageBox.Show("Файл невозможно открыть повторно", "Ошибка");
            }
        }

        private void ShowDiscipB_Click(object sender, RoutedEventArgs e)
        {
            if (FindDiscipTB.Text != "")
            {
                List<Exam> find_res = semester.FindAll(x => x.Discipline.ToLower() == FindDiscipTB.Text.ToLower());
                FindDiscipTB.Text = "";

                ResetGrid(find_res);
            }
            else
            {
                MessageBox.Show("Необходимо ввести название предмета", "Ошибка");
            }
        }

        private void ResetB_Click(object sender, RoutedEventArgs e)
        {
            ResetGrid();
        }

        private void ResetGrid(IEnumerable list)
        {
            //ExamsGrid.ItemsSource = null;
            //ExamsGrid.ItemsSource = list;

            ExamsGrid.Items.Clear();
            foreach (var item in list)
            {
                ExamsGrid.Items.Add(item);
            }
        }
        private void ResetGrid()
        {
            //ExamsGrid.ItemsSource = null;
            //ExamsGrid.ItemsSource = semester.Exams;

            ExamsGrid.Items.Clear();
            foreach (var item in semester.Exams)
            {
                ExamsGrid.Items.Add(item);
            }
        }

        private void RemoveDiscipB_Click(object sender, RoutedEventArgs e)
        {
            if (RemoveDiscipTB.Text != "")
            {
                semester.RemoveAll(x => x.Discipline.ToLower() == RemoveDiscipTB.Text.ToLower());
                RemoveDiscipTB.Text = "";

                ResetGrid();
            }
            else
            {
                MessageBox.Show("Необходимо ввести название предмета", "Ошибка");
            }
        }

        private void FindDateB_Click(object sender, RoutedEventArgs e)
        {
            int sel = FindDateCB.SelectedIndex;
            List<Exam> find_res;
            if (sel == 0)
            {
                find_res = semester.FindAll(x => x.ExamDate < FindDateDP.SelectedDate);
            }
            else if (sel == 1)
            {
                find_res = semester.FindAll(x => x.ExamDate > FindDateDP.SelectedDate);
            }
            else
            {
                find_res = semester.FindAll(x => x.ExamDate == FindDateDP.SelectedDate);
            }
            FindDateCB.SelectedIndex = -1;
            ResetGrid(find_res);
        }

        private void RemoveDateB_Click(object sender, RoutedEventArgs e)
        {
            int sel = RemoveDateCB.SelectedIndex;
            if (sel == 0)
            {
                semester.RemoveAll(x => x.ExamDate < RemoveDateDP.SelectedDate);
            }
            else if (sel == 1)
            {
                semester.RemoveAll(x => x.ExamDate > RemoveDateDP.SelectedDate);
            }
            else
            {
                semester.RemoveAll(x => x.ExamDate == RemoveDateDP.SelectedDate);
            }
            RemoveDateCB.SelectedIndex = -1;
            ResetGrid();
        }

        private void ExamsGrid_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {

        }
    }
}
