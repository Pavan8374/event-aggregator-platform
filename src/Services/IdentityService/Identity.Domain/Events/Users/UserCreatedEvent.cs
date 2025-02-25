using Identity.Domain.Common;

namespace Identity.Domain.Events.Users
{
    public class UserCreatedEvent : DomainEvent
    {
        public Guid UserId { get; }
        public string Email { get; }

        public UserCreatedEvent(Guid userId, string email)
        {
            UserId = userId;
            Email = email;
        }
    }
}
