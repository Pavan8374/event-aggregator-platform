using Event.Domain.Common;

namespace Event.Domain.ValueObjects.Events
{
    public class EventDescription : ValueObject
    {
        public string Value { get; private set; }

        private EventDescription(string value)
        {
            Value = value;
        }

        public static EventDescription Create(string description)
        {
            return new EventDescription(description ?? string.Empty);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
