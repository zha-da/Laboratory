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
using LaboratoryMainApp.Tabs_and_Windows;

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

            string[] vs = new string[] { ">", "<", "=", "All" };

            foreach (var op in vs)
            {
                RadioButton rb = new RadioButton { Content = op };
                rb.Checked += (s, ev) => dateFilter = RbChosen(op);

                DateWP.Children.Add(rb);
            }
        }

        HashSet<string> openedFiles = new HashSet<string>(); //хранит имена уже открытых файлов
        static internal Semester semester = new Semester(new List<Exam>());

        Predicate<Exam> dateFilter;
        Predicate<Exam> filter;
        public Predicate<Exam> Filter
        {
            get => filter;
            set
            {
                filter = value;
            }
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            FilterBtn.IsEnabled = true;
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
            FilterBtn.IsEnabled = false;
            fbp = 0;
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
            }
            else
            {
                MessageBox.Show("Файл невозможно открыть повторно", "Ошибка");
            }
        }


        private void ResetB_Click(object sender, RoutedEventArgs e)
        {
            filter = null;
            ResetGrid();
        }

        private void ResetGrid(IEnumerable list)
        {

            ExamsGrid.Items.Clear();
            foreach (var item in list)
            {
                ExamsGrid.Items.Add(item);
            }
        }
        private void ResetGrid()
        {

            ExamsGrid.Visibility = Visibility.Visible;

            ExamsGrid.Items.Clear();
            foreach (var item in semester.Exams)
            {
                ExamsGrid.Items.Add(item);
            }
        }


        private void AddByHandButton_Click(object sender, RoutedEventArgs e)
        {
            AddItemWindow win = new AddItemWindow();
            win.ShowDialog();

            ResetGrid();
        }

        int fbp = 0;

        private void FilterBtn_Click(object sender, RoutedEventArgs e)
        {
            fbp++;
            fbp = fbp % 2;

            if (fbp == 0)
            {
                FiltersSP.Visibility = Visibility.Collapsed;
                DisciplinecWP.Children.Clear();
                return;
            }

            FiltersSP.Visibility = Visibility.Visible;

            var discips = (from ex in semester select ex.Discipline).Distinct();

            foreach (var ex in discips)
            {
                CheckBox cb = new CheckBox() { Content = ex };
                Predicate<Exam> con = exam => exam.Discipline.ToLower() == ex.ToLower();
                cb.Checked += (s, ev) => Filter += con;
                cb.Unchecked += (s, ev) => Filter -= con;

                DisciplinecWP.Children.Add(cb);
            }
        }
        Predicate<Exam> RbChosen(string op)
        {
            DateTime sDate;
            Predicate<Exam> res;
            if (!ByDateDP.SelectedDate.HasValue)
            {
                return null;
            }

            sDate = (DateTime)ByDateDP.SelectedDate;
            switch (op)
            {
                case ">":
                    res = x => x.ExamDate > sDate;
                    break;
                case "<":
                    res = x => x.ExamDate < sDate;
                    break;
                case "=":
                    res = x => x.ExamDate == sDate;
                    break;
                default:
                    res = null;
                    break;
            }
            return res;
        }

        private void ApplyFiltersBtn_Click(object sender, RoutedEventArgs e)
        {
            Semester res = semester.FindOr(filter);
            res = res.FindAnd(dateFilter);
            ResetGrid(res);
        }

        private void NullFiltersBtn_Click(object sender, RoutedEventArgs e)
        {
            Filter = null;
            dateFilter = null;
            foreach (var item in DateWP.Children)
            {
                if (item is RadioButton)
                    ((RadioButton)item).IsChecked = false;
            }
            ResetGrid();
        }
    }
}
