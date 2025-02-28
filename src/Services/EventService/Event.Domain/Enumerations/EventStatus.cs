using Event.Domain.Common;

namespace Event.Domain.Enumerations
{
    public class EventStatus : Enumeration
    {
        public static EventStatus Pending = new EventStatus(1, "PENDING");
        public static EventStatus Approved = new EventStatus(2, "APPROVED");
        public static EventStatus Cancelled = new EventStatus(3, "CANCELLED");

        public EventStatus(int id, string name) : base(id, name)
        {
        }
    }
}
