namespace Event.Api.Models
{
    public class Event
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; }
        public int MaxAttendees { get; set; }
        public List<Attendee> Attendees { get; set; }
    }
}
