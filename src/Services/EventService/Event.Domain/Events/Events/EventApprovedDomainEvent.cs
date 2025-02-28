using Event.Domain.Common;

namespace Event.Domain.Events.Events
{
    public class EventApprovedDomainEvent : DomainEvent
    {
        public Guid EventId { get; set; }
        public EventApprovedDomainEvent(Guid eventId)
        {
            EventId = eventId;
        }
    }
}
