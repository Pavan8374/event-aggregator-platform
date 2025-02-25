using Event.Domain.Common;

namespace Event.Domain.Enumerations
{
    public class Currency : Enumeration
    {
        public static Currency INR = new Currency(1, "INR");
        public static Currency USD = new Currency(2, "USD");
        public static Currency EUR = new Currency(3, "EUR");
        public static Currency GBP = new Currency(4, "GBP");
        // Add more currencies as needed

        public Currency(int id, string name) : base(id, name)
        {
        }
    }
}
