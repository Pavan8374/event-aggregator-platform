using Event.Domain.Common;

namespace Event.Domain.Events.Events
{
    public class EventCancelledDomainEvent : DomainEvent
    {
        public Guid EventId { get; set; }

        public EventCancelledDomainEvent(Guid eventId)
        {
            EventId = eventId;
        }
    }
}
