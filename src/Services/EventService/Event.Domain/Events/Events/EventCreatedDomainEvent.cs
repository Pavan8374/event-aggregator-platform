using Event.Domain.Common;

namespace Event.Domain.Events.Events
{
    public class EventCreatedDomainEvent : DomainEvent
    {
        public Guid EventId { get; set; }
        public Guid OrganizerId { get; set; }
        public EventCreatedDomainEvent(Guid eventId, Guid organizerId)
        {
            EventId = eventId;
            OrganizerId = organizerId;
        }
    }
}
