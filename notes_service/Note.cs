using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace notes_service
{
    //public class NotesUser
    //{
    //    public string UserName { get; set; }
    //    public List<Note> Notes { get; set; }

    //    public NotesUser(string name)
    //    {
    //        UserName = name;
    //        Notes = new List<Note>();
    //    }
    //    public NotesUser()
    //    {
    //        Notes = new List<Note>();
    //    }
    //}
    public class Note
    {
        public string Text { get; set; }
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
