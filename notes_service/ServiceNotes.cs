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
        //static List<string> usernames = new List<string>();

        public List<Note> ConnectToService(string username)
        {
            try
            {
                DataContractSerializer dcs = new DataContractSerializer(typeof(List<Note>));
                using (FileStream fs = new FileStream($"C:/Users/Pepe/Visual Studio Projects/Laba/notes_service/bin/Debug/{username}.xml", FileMode.Open))
                {
                    return (List<Note>)dcs.ReadObject(fs);
                }
            }
            catch (FileNotFoundException)
            {
                return null;
            }
            catch (SerializationException)
            {
                return null;
            }
        }

        public Note CreateNote(string text)
        {
            Note nn = new Note(text);
            return nn;
        }

        public void DisconnectFromService(string username, List<Note> notes)
        {
            DataContractSerializer dcs = new DataContractSerializer(typeof(List<Note>));
            using (FileStream fs = new FileStream($"C:/Users/Pepe/Visual Studio Projects/Laba/notes_service/bin/Debug/{username}.xml", FileMode.OpenOrCreate))
            {
                dcs.WriteObject(fs, notes);
            }
        }
    }
    [DataContract]
    public class Note
    {
        [DataMember]
        public string Text { get; set; }
        [DataMember]
        public DateTime CreationTime { get; set; }

        public Note(string text)
        {
            Text = text;
            CreationTime = DateTime.Now;
        }
        public Note()
        {
            CreationTime = DateTime.Now;
        }
        public override string ToString()
        {
            return $"{CreationTime.ToShortDateString()} {CreationTime.Hour}:{CreationTime.Minute} : {Text}";
        }
    }
}
