using System;

namespace FindieServer.DbModels
{
    public partial class EventComments
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }
        public string Comment { get; set; }
        public DateTime TimeStamp { get; set; }

        public Events Event { get; set; }
    }
}
