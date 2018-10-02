using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FindieServer.Models.AdminsModel
{
    [Table("NotepadTable")]
    public class Notepads
    {       
        public int Id { get; set; }
        [StringLength(20)]
        public string Username { get; set; }
        [StringLength(1000)]
        public string NotepadContent { get; set; }
        [StringLength(50)]
        public string Title { get; set; }
        [Column(TypeName = "date")]
        public DateTime TimeStamp { get; set; }
    }
}
