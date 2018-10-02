using System;

namespace FindieServer.Models
{
    public class EventCommentModel
    {
        public int EventId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Comment { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}