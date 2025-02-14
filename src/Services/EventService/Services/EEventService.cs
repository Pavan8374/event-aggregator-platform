using EventService.Models;
using EventService.Services.Interfaces;

namespace EventService.Services
{
    public class EEventService : IEventService
    {
        private List<Event> events = new List<Event>
        {
            new Event
            {
                Id = Guid.NewGuid(),
                Name = "Tech Conference",
                Description = "A conference about the latest in tech.",
                StartDate = new DateTime(2025, 3, 15),
                EndDate = new DateTime(2025, 3, 16),
                Location = "Convention Center",
                MaxAttendees = 500,
                Attendees = new List<Attendee>()
            },
            new Event
            {
                Id = Guid.NewGuid(),
                Name = "Art Exhibition",
                Description = "An exhibition showcasing local artists.",
                StartDate = new DateTime(2025, 4, 10),
                EndDate = new DateTime(2025, 4, 12),
                Location = "Art Gallery",
                MaxAttendees = 200,
                Attendees = new List<Attendee>()
            }
        };

        public Task<Event> GetEventByIdAsync(Guid id)
        {
            var eventItem = events.Find(e => e.Id == id);
            return Task.FromResult(eventItem);
        }

        public Task<IEnumerable<Event>> GetEventsAsync()
        {
            return Task.FromResult<IEnumerable<Event>>(events);
        }

        public Task<IEnumerable<Event>> GetEventsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var filteredEvents = events.FindAll(e => e.StartDate >= startDate && e.EndDate <= endDate);
            return Task.FromResult<IEnumerable<Event>>(filteredEvents);
        }
    }
}
