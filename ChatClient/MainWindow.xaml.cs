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
using ChatClient.NotesService;

namespace ChatClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Closing += (s, e) => client.DisconnectFromService(tbUsername.Text);
            //Loaded += (s, e) => client = new ServiceNotesClient();
        }

        ServiceNotesClient client;
        List<Note> notes = new List<Note>();
        private void bConnect_Click(object sender, RoutedEventArgs e)
        {
            //NotesService.Service1Client client = new NotesService.Service1Client();
            //string returnstring;

            //returnstring = client.GetData(tbUsername.Text);
            //((Button)sender).Content = returnstring;

            //client.Close();

            if (bConnect.Content.ToString().Equals("Подключиться"))
            {
                tbUsername.IsEnabled = false;
                bCreate.IsEnabled = true;
                bConnect.Content = "Отключиться";
                client = new ServiceNotesClient();
                Note[] ns = client.ConnectToService(tbUsername.Text);
                if (ns == null)
                {
                    return;
                }

                notes.AddRange(ns);
                foreach (var n in notes)
                {
                    lbNotes.Items.Add($"{n.CreationTime.ToShortDateString()} {n.CreationTime.Hour}:{n.CreationTime.Minute} : {n.Text}");
                }
            }
            else if (bConnect.Content.ToString().Equals("Отключиться"))
            {
                tbUsername.IsEnabled = true;
                bCreate.IsEnabled = false;
                bConnect.Content = "Подключиться";
                lbNotes.Items.Clear();
                try
                {
                    client.DisconnectFromService(tbUsername.Text, notes.ToArray());
                }
                catch (System.ServiceModel.ProtocolException)
                {
                }
                finally
                {
                    notes.Clear();
                }
            }
        }

        private void bCreate_Click(object sender, RoutedEventArgs e)
        {
            tbNote.IsEnabled = true;
            tbNote.Focus();
        }

        private void tbNote_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Note n = client.CreateNote(tbNote.Text);
                notes.Add(n);
                lbNotes.Items.Add($"{n.CreationTime.ToShortDateString()} {n.CreationTime.Hour}:{n.CreationTime.Minute} : {n.Text}");
                tbNote.Text = "";
                tbNote.IsEnabled = false;
            }
        }
    }
}
