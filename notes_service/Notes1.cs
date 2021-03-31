using System.Collections.Generic;

namespace notes_service
{
    public class Notes1
    {
        public List<Note> AllNotes { get; set; }
        public Notes1()
        {
            AllNotes = new List<Note>();
        }
    }
}
