using Event.Domain.Common;

namespace Event.Domain.ValueObjects.Events
{
    public class EventTitle : ValueObject
    {
        public string Value { get; private set; }

        private EventTitle(string value)
        {
            Value = value;
        }

        public static EventTitle Create(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Event title cannot be empty", nameof(title));

            if (title.Length > 255)
                throw new ArgumentException("Event title cannot exceed 255 characters", nameof(title));

            return new EventTitle(title);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
