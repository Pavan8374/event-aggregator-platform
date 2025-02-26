using Event.Api.Models;

namespace Event.Api.Services.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<Event.Api.Models.Event>> GetEventsAsync();
        Task<Event.Api.Models.Event> GetEventByIdAsync(Guid id);
        Task<IEnumerable<Event.Api.Models.Event>> GetEventsByDateRangeAsync(DateTime startDate, DateTime endDate);
    }
}
