using MediatR;

namespace Event.Domain.Common
{
    public abstract class DomainEvent : INotification
    {
        public Guid Id { get; }
        public DateTime OccuredOn { get; }

        protected DomainEvent()
        {
            Id = Guid.NewGuid();
            OccuredOn = DateTime.UtcNow;
        }
    }
}
