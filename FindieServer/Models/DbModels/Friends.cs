using System.ComponentModel.DataAnnotations.Schema;

namespace FindieServer.Models.DbModels
{
    [Table("FriendTable")]
    public partial class Friends
    {
        public int Id { get; set; }

        public int? FirstUser { get; set; }

        public int? SecondUser { get; set; }

        public bool IsRequestAccepted { get; set; }
    }
}