using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml.Serialization;

namespace notes_service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServiceNotes" in both code and config file together.
    public class ServiceNotes : IServiceNotes
    {
        List<Note> allnotes = new List<Note>();
        static List<string> usernames = new List<string>();

        public List<Note> ConnectToService(string username)
        {
            if (!usernames.Contains(username))
            {
                usernames.Add(username);
                return null;
            }
            XmlSerializer serializer = new XmlSerializer(typeof(List<Note>));
            using (FileStream fs = new FileStream($"C:/Users/Pepe/Visual Studio Projects/Laba/notes_service/bin/Debug/{username}.xml", FileMode.Open))
            {
                allnotes = (List<Note>)serializer.Deserialize(fs);
                return allnotes;
            }
        }

        public Note CreateNote(string text)
        {
            Note nn = new Note(text);
            allnotes.Add(nn);
            return nn;
        }

        public void DisconnectFromService(string username)
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<Note>));
            using (FileStream fs = new FileStream($"C:/Users/Pepe/Visual Studio Projects/Laba/notes_service/bin/Debug/{username}.xml", FileMode.OpenOrCreate))
            {
                xs.Serialize(fs, allnotes);
            }
        }
    }
}
