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
using System.Reflection;

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

        Assembly asm = null;

        List<IPlugable> plugins = new List<IPlugable>();

        bool AssemblyLoaded(string path, out List<Type> types_res)
        {
            bool loaded = false;

            try
            {
                asm = Assembly.LoadFrom(path);
            }
            catch (Exception ex)
            {
                Logger.NewLog(ex.Message);
                MessageBox.Show(ex.Message);
                types_res = null;
                return loaded;
            }

            Type[] types = asm.GetTypes();
            types_res = new List<Type>();
            for (int i = 0; i < types.Length; i++)
            {
                Type t = types[i].GetInterface("IPlugable");
                if (t != null)
                {
                    loaded = true;
                    types_res.Add(types[i]);
                }
            }
            return loaded;
        }

        private void AddAsmBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Файлы сборок|*.dll";
            ofd.ShowDialog();

            try
            {
                List<Type> res;
                if (!AssemblyLoaded(ofd.FileName, out res))
                {
                    MessageBox.Show("Ни один элемент сборки не реализует интерфейс IPlugable");
                    return;
                }
                foreach (var type in res)
                {
                    object o = asm.CreateInstance(type.FullName);
                    IPlugable plug = o as IPlugable;
                    plugins.Add(plug);
                }
            }
            catch (Exception ex)
            {
                Logger.NewLog(ex.Message);
            }
        }

        private void PrintBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (var plug in plugins)
            {
                foreach (var item in plug.Print())
                {
                    if (!AsmLoadResult.Items.Contains(item))
                    {
                        AsmLoadResult.Items.Add(item); 
                    }
                }
            }
        }

        private void SortStrBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (var plug in plugins)
            {
                plug.SortByString();
            }
            Refresh();
        }

        private void SortIntBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (var plug in plugins)
            {
                plug.SortByInt();
            }
            Refresh();
        }
        void Refresh()
        {
            AsmLoadResult.Items.Clear();
            PrintBtn_Click(null, null);
        }
    }
}
