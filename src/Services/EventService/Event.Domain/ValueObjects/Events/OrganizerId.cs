using Event.Domain.Common;

namespace Event.Domain.ValueObjects.Events
{
    public class OrganizerId : ValueObject
    {
        public Guid Value { get; private set; }

        private OrganizerId(Guid value)
        {
            Value = value;
        }

        public static OrganizerId From(Guid id) => new OrganizerId(id);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
