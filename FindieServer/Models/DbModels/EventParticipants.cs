namespace FindieServer.Models.DbModels
{
    public partial class EventParticipants
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int UserId { get; set; }
        public bool? IsUserAcceptedEventRequest { get; set; }

        public Events Event { get; set; }
    }
}
