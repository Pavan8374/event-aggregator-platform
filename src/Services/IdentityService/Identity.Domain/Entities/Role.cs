using Identity.Domain.Common;
using Identity.Domain.Enumerations;

namespace Identity.Domain.Entities
{
    public class Role : AggregateRoot
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public RoleType Type { get; private set; }

        // For EF Core
        private Role() { }

        public Role(string name, string description, RoleType type)
        {
            Name = name;
            Description = description;
            Type = type;
        }

        public void Update(string name, string description)
        {
            Name = name;
            Description = description;
        }

        // Factory method to create a role with a specific type
        public static Role Create(string name, string description, RoleType type)
        {
            return new Role(name, description, type);
        }

        // Factory method to create default roles based on RoleType
        public static Role CreateDefault(RoleType type)
        {
            string description = $"Default {type.Name} role";
            return new Role(type.Name, description, type);
        }
    }
}
