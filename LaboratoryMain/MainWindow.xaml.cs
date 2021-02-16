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

namespace LaboratoryMain
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string s_flag = ".dat";
        public string SFlag 
        {
            get => s_flag;
            set
            {
                s_flag = value;
                ExtBox.Text = value;
            }
        }
        public MainWindow()
        {
            InitializeComponent();

            s_bin.Click += (s, e) => SFlag = ".dat";
            s_xml.Click += (s, e) => SFlag = ".xml";
            s_json.Click += (s, e) => SFlag = ".json";
        }
        string folderpath;
        UDirectory directory;
        private void ChooseBth_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                folderpath = fbd.SelectedPath;
                directory = new UDirectory(folderpath);
            }
        }

        private void SerializeBth_Click(object sender, RoutedEventArgs e)
        {
            string filename;
            System.Windows.Forms.OpenFileDialog fbd = new System.Windows.Forms.OpenFileDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filename = fbd.FileName;
            }
            else return;

            Serialize(directory, filename);

            System.Windows.Forms.MessageBox.Show("Сериализация завершена");
        }

        private void DeserializeBtn_Click(object sender, RoutedEventArgs e)
        {
            string filename;
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filename = ofd.FileName;
            }
            else return;

            UDirectory dir = Deserialize(filename);

            string path;
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                path = fbd.SelectedPath;
            }
            else return;

            CreateFolders(dir, path);

            System.Windows.Forms.MessageBox.Show("Десериализация завершена");
        }

        private void CreateFolders(UDirectory dir, string path)
        {
            DirectoryInfo dinfo = new DirectoryInfo(path);
            dinfo.CreateSubdirectory(dir.Name);

            dinfo = new DirectoryInfo($"{path}\\{dir.Name}");
            dinfo.LastWriteTime = dir.CreationTime;

            foreach (var item in dir.Files)
            {
                CreateFiles(item, $"{path}\\{dir.Name}\\{item.Name}");
            }
            foreach (var item in dir.SubDirectories)
            {
                CreateFolders(item, $"{path}\\{dir.Name}");
            }
        }
        private void CreateFiles(UFile file ,string path)
        {
            File.WriteAllBytes(path, file.Data);

            FileInfo fi = new FileInfo(path);
            fi.Attributes = file.Attributes;
            fi.LastWriteTime = file.ModificationTime;
        }
        private void Serialize(UDirectory dir, string filename)
        {
            switch (SFlag)
            {
                case ".dat":
                    BinaryFormatter bf = new BinaryFormatter();
                    using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
                    {
                        bf.Serialize(fs, directory);
                    }
                    return;
                case ".xml":
                    XmlSerializer serializer = new XmlSerializer(typeof(UDirectory));
                    using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
                    {
                        serializer.Serialize(fs, directory);
                    }
                    return;
                case ".json":
                    string res = JsonSerializer.Serialize<UDirectory>(dir);
                    using (StreamWriter sw = new StreamWriter(filename))
                    {
                        sw.WriteLine(res);
                    }
                    return;
                default:
                    break;
            }
        }
        private UDirectory Deserialize(string filename)
        {
            FileInfo fi = new FileInfo(filename);
            switch (fi.Extension)
            {
                case ".dat":
                    BinaryFormatter bf = new BinaryFormatter();
                    using (FileStream fs = new FileStream(filename, FileMode.Open))
                    {
                        return (UDirectory)bf.Deserialize(fs);
                    }
                case ".xml":
                    XmlSerializer xf = new XmlSerializer(typeof(UDirectory));
                    using (FileStream fs = new FileStream(filename, FileMode.Open))
                    {
                        return (UDirectory)xf.Deserialize(fs);
                    }
                case ".json":
                    using (StreamReader sr = new StreamReader(filename))
                    {
                        return JsonSerializer.Deserialize<UDirectory>((sr.ReadToEnd()).ToString());
                    }
                default: return null;
            }
        }
    }
}
