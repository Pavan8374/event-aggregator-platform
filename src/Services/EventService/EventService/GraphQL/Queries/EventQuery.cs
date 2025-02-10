namespace EventService.GraphQL.Queries
{
    public class EventQuery
    {
        public List<EventDto> GetEvents() => new()
    {
        new EventDto { Id = 1, Name = "Tech Conference", Location = "New York" },
        new EventDto { Id = 2, Name = "Gaming Expo", Location = "Los Angeles" }
    };
    }

    public class EventDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
    }
}
