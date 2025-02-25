using Event.Domain.Common;
using MediatR;

namespace Event.Domain.Events.Common
{
    public interface IDomainEventHandler<in TEvent> : INotificationHandler<TEvent>
        where TEvent : DomainEvent
    {
    }
}
