using Event.Domain.Common;
using Event.Domain.Enumerations;

namespace Event.Domain.ValueObjects.Events
{
    public class Money : ValueObject
    {
        public decimal Amount { get; private set; }
        public Currency Currency { get; private set; }

        private Money(decimal amount, Currency currency)
        {
            Amount = amount;
            Currency = currency;
        }
            
        public static Money Create(decimal amount, Currency currency)
        {
            if (amount < 0)
                throw new ArgumentException("Amount cannot be negative", nameof(amount));

            return new Money(Math.Round(amount, 2), currency);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
            yield return Currency;
        }
    }
}
