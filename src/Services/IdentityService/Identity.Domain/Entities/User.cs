using Identity.Domain.Common;
using Identity.Domain.Events.Users;
using Identity.Domain.ValueObjects.Users;

namespace Identity.Domain.Entities
{
    public class User : AggregateRoot
    {
        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Email Email { get; private set; }
        public Password Password { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? LastLoginAt { get; private set; }
        public bool IsActive { get; private set; }
        public int RoleId { get; private set; }
        private User() { } // For EF Core

        private User(string firstName, string lastName, Email email, Password password, int roleId)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
            RoleId = roleId;

            AddDomainEvent(new UserCreatedEvent(Id, Email.Value));
        }


        public static User Create(string firstName, string lastName, string email, string password, int roleId)
        {
            var emailVO = Email.Create(email);
            var passwordVO = Password.Create(password);

            return new User(firstName, lastName, emailVO, passwordVO, roleId);
        }

        public void UpdateDetails(string firstName, string lastName, string email)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new DomainException("First name cannot be empty");

            if (string.IsNullOrWhiteSpace(lastName))
                throw new DomainException("Last name cannot be empty");

            FirstName = firstName;
            LastName = lastName;
        }

        public bool VerifyPassword(string password)
        {
            return Password.VerifyPassword(password);
        }

        public void ChangePassword(string currentPassword, string newPassword)
        {
            if (!VerifyPassword(currentPassword))
                throw new DomainException("Current password is incorrect");

            Password = Password.Create(newPassword);

            //AddDomainEvent(new PasswordChangedEvent(Id)); //need to implement PasswordChangedEvent
        }

        public void ChangeRole(int roleId)
        {
            if (roleId <= 0)
                throw new DomainException("Invalid role ID");

            RoleId = roleId;
            // Optionally add domain event for role change
            // AddDomainEvent(new UserRoleChangedEvent(Id, RoleId));
        }

        //public void Deactivate()
        //{
        //    IsActive = false;
        //    AddDomainEvent(new UserDeactivatedEvent(Id));
        //}

        public void RecordLogin()
        {
            LastLoginAt = DateTime.UtcNow;
        }
    }
}
