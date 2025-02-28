using Event.Domain.Common;

namespace Event.Domain.ValueObjects.Events
{
    public class Capacity : ValueObject
    {
        public int TotalSeats { get; private set; }
        public int AvailableSeats { get; private set; }

        private Capacity(int totalSeats, int availableSeats)
        {
            TotalSeats = totalSeats;
            AvailableSeats = availableSeats;
        }

        public static Capacity Create(int totalSeats)
        {
            if (totalSeats < 0)
                throw new ArgumentException("Total seats cannot be negative", nameof(totalSeats));

            return new Capacity(totalSeats, totalSeats);
        }

        public Capacity ReserveSeats(int numberOfSeats)
        {
            if (numberOfSeats < 0)
                throw new ArgumentException("Number of seats cannot be negative", nameof(numberOfSeats));

            if (numberOfSeats > AvailableSeats)
                throw new InvalidOperationException("Not enough available seats");

            return new Capacity(TotalSeats, AvailableSeats - numberOfSeats);
        }

        public Capacity ReleaseSeats(int numberOfSeats)
        {
            if (numberOfSeats < 0)
                throw new ArgumentException("Number of seats cannot be negative", nameof(numberOfSeats));

            int newAvailableSeats = AvailableSeats + numberOfSeats;
            if (newAvailableSeats > TotalSeats)
                throw new InvalidOperationException("Cannot release more seats than total capacity");

            return new Capacity(TotalSeats, newAvailableSeats);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return TotalSeats;
            yield return AvailableSeats;
        }
    }
}
