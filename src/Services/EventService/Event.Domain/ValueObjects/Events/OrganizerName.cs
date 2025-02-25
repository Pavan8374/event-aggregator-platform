using Event.Domain.Common;

namespace Event.Domain.ValueObjects.Events
{
    public class OrganizerName : ValueObject
    {
        public string Value { get; private set; }

        private OrganizerName(string value)
        {
            Value = value;
        }

        public static OrganizerName Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Organizer name cannot be empty", nameof(name));

            if (name.Length > 255)
                throw new ArgumentException("Organizer name cannot exceed 255 characters", nameof(name));

            return new OrganizerName(name);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
