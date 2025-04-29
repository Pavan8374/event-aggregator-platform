using Identity.Domain.Common;

namespace Identity.Domain.Events.Users
{
    /// <summary>
    /// User created event
    /// </summary>
    public class UserCreatedEvent : DomainEvent
    {
        /// <summary>
        /// User identity
        /// </summary>
        public Guid UserId { get; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; }

        /// <summary>
        /// User created event
        /// </summary>
        /// <param name="userId">User identity</param>
        /// <param name="email">Email</param>
        public UserCreatedEvent(Guid userId, string email)
        {
            UserId = userId;
            Email = email;
        }
    }
}
