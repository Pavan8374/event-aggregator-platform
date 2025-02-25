using Event.Domain.Common;

namespace Event.Domain.ValueObjects.Events
{
    public class Venue : ValueObject
    {
        public string Value { get; private set; }

        private Venue(string value)
        {
            Value = value;
        }

        public static Venue Create(string venue)
        {
            if (string.IsNullOrWhiteSpace(venue))
                throw new ArgumentException("Venue cannot be empty", nameof(venue));

            if (venue.Length > 255)
                throw new ArgumentException("Venue cannot exceed 255 characters", nameof(venue));

            return new Venue(venue);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
