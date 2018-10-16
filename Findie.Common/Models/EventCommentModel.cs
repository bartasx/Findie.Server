using System;

namespace Findie.Common.Models
{
    public class EventCommentModel
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Comment { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}