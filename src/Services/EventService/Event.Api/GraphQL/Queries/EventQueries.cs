using Event.Api.Services.Interfaces;

namespace Event.GraphQL.Queries
{
    [ExtendObjectType("Query")]
    public class EventQueries
    {
        private readonly IEventService _eventService;

        public EventQueries(IEventService eventService)
        {
            _eventService = eventService;
        }

        [GraphQLDescription("Gets all events")]
        public async Task<IEnumerable<Event.Api.Models.Event>> GetEvents()
        {
            return await _eventService.GetEventsAsync();
        }

        [GraphQLDescription("Gets an event by ID")]
        public async Task<Event.Api.Models.Event> GetEvent(Guid id)
        {
            return await _eventService.GetEventByIdAsync(id);
        }

        [GraphQLDescription("Gets events by date range")]
        public async Task<IEnumerable<Event.Api.Models.Event>> GetEventsByDateRange(
            DateTime startDate,
            DateTime endDate)
        {
            return await _eventService.GetEventsByDateRangeAsync(startDate, endDate);
        }
    }
}