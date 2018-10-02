using System;
using System.Collections.Generic;

namespace FindieServer.Models.DbModels
{
    public partial class Events
    {
        public Events()
        {
            EventComments = new HashSet<EventComments>();
            EventParticipants = new HashSet<EventParticipants>();
        }

        public int Id { get; set; }
        public int HostId { get; set; }
        public string EventDescription { get; set; }
        public DateTime DateOfCreate { get; set; }
        public DateTime? EventDate { get; set; }
        public bool IsEventPrivate { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string ImageUri { get; set; }
        public string EventName { get; set; }

        public ICollection<EventComments> EventComments { get; set; }
        public ICollection<EventParticipants> EventParticipants { get; set; }
    }
}