using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FindieServer.DbModels
{
    public partial class Messages
    {
        public int Id { get; set; }

        public int MessageFrom { get; set; }

        public int MessageTo { get; set; }

        [Column(TypeName = "date")]
        public DateTime TimeStamp { get; set; }

        [Required]
        [StringLength(250)]
        public string MessageText { get; set; }
    }
}