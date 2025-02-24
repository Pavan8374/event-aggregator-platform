using Event.Domain.Common;

namespace Event.Domain.Events.Common
{
    public interface IDomainEventService
    {
        Task PublishAsync(DomainEvent domainEvent);
        Task PublishAsync(IEnumerable<DomainEvent> domainEvents);
    }
}
