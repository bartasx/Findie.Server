using System;
using Microsoft.AspNetCore.Http;

namespace Findie.Common.Models
{
    public class EventModel
    {
        public int EventId { get; set; }
        public string HostUsername { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public DateTime DateOfEvent { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public IFormFile File { get; set; }
    }
}