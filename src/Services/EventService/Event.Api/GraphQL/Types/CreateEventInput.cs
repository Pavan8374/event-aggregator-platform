using Event.Domain.Enumerations;
using Event.Domain.ValueObjects.Events;

namespace Event.Api.GraphQL.Types
{
    public class CreateEventInput
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public EventCategory Category { get; set; }
        public string Venue { get; set; }
        public EventTimeRange TimeRange { get; set; }
        public Capacity Capacity { get; set; }
        public Money TicketPrice { get; set; }
        public bool IsFree { get; set; }

        [GraphQLType(typeof(UploadType))]
        public IFile Thumbnail { get; set; }
    }
}
