using Event.Domain.Common;

namespace Event.Domain.Events.Events
{
    public class EventUpdatedDomainEvent : DomainEvent
    {
        public Guid EventId { get; set; }

        public EventUpdatedDomainEvent(Guid eventId)
        {
            EventId = eventId;  
        }
    }
}
