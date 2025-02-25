using Event.Domain.Common;

namespace Event.Domain.ValueObjects.Events
{
    public class EventTimeRange : ValueObject
    {
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }

        private EventTimeRange(DateTime startTime, DateTime endTime)
        {
            StartTime = startTime;
            EndTime = endTime;
        }

        public static EventTimeRange Create(DateTime startTime, DateTime endTime)
        {
            if (startTime >= endTime)
                throw new ArgumentException("Start time must be before end time");

            return new EventTimeRange(startTime, endTime);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return StartTime;
            yield return EndTime;
        }
    }
}
