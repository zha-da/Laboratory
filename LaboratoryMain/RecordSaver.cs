using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LaboratoryMain
{
    public static class RecordSaver
    {
        public static List<RecordPair> GetRecords(string gamemode)
        {
            string file = "recordsClassic.xml";
            switch (gamemode)
            {
                case "classic":
                    break;
                case "endless":
                    file = "recordsEnemiesEndless.xml";
                    break;
                case "sbdestroyer":
                    file = "recordsEnemiesSbDest.xml";
                    break;
                case "sbclicks":
                    file = "recordsClicks.xml";
                    break;
            }

            XmlSerializer xf = new XmlSerializer(typeof(List<RecordPair>));
            try
            {
                string local = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                using (FileStream fs = new FileStream($"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}/SpaceInvaders/{file}", FileMode.Open))
                {
                    return (List<RecordPair>)xf.Deserialize(fs);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static void SaveRecords(List<RecordPair> records, string gamemode, string name, int value)
        {
            string file = "recordsClassic.xml";
            switch (gamemode)
            {
                case "classic":
                    break;
                case "endless":
                    file = "recordsEnemiesEndless.xml";
                    break;
                case "sbdestroyer":
                    file = "recordsEnemiesSbDest.xml";
                    break;
                case "sbclicks":
                    file = "recordsClicks.xml";
                    break;
            }


            if (records == null)
            {
                records = new List<RecordPair>();
            }

            if (records.Count < 5)
            {
                records.Add(new RecordPair(name, value));
                records.Sort();
            }
            else if (records[4].Value < value)
            {
                records.RemoveAt(4);
                records.Add(new RecordPair(name, value));
                records.Sort();
            }

            XmlSerializer serializer = new XmlSerializer(typeof(List<RecordPair>));
            using (FileStream fs = new FileStream($"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}/SpaceInvaders/{file}", FileMode.OpenOrCreate))
            {
                serializer.Serialize(fs, records);
            }
        }

    }

    [Serializable]
    public class RecordPair : IComparable<RecordPair>
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public RecordPair()
        {

        }
        public RecordPair(string name, int value)
        {
            Name = name;
            Value = value;
        }
        public int CompareTo(RecordPair other)
        {
            return -Value.CompareTo(other.Value);
        }
        public override string ToString()
        {
            return Name + " " + Value;
        }
    }
}
