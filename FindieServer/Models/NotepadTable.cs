using System;

namespace FindieServer.Models
{
    public partial class NotepadTable
    {
        public int Id { get; set; }
        public string NotepadContent { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Title { get; set; }
        public string Username { get; set; }
    }
}
