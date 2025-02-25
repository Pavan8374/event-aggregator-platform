using Event.Domain.Common;
using Event.Domain.Enumerations;
using Event.Domain.Events.Events;
using Event.Domain.ValueObjects.Events;

namespace Event.Domain.Entities
{
    public class Event : AggregateRoot
    {
        // Properties
        public EventTitle Title { get; private set; }
        public EventDescription Description { get; private set; }
        public EventCategory Category { get; private set; }
        public OrganizerId OrganizerId { get; private set; }
        public OrganizerName OrganizerName { get; private set; }
        public Venue Venue { get; private set; }
        public EventTimeRange TimeRange { get; private set; }
        public Capacity Capacity { get; private set; }
        public Money TicketPrice { get; private set; }
        public bool IsFree { get; private set; }
        public string ImageUrl { get; private set; }
        public EventStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        // Constructor for creating a new event
        private Event() { }

        public static Event Create(
            EventTitle title,
            EventDescription description,
            EventCategory category,
            OrganizerId organizerId,
            OrganizerName organizerName,
            Venue venue,
            EventTimeRange timeRange,
            Capacity capacity,
            Money ticketPrice,
            bool isFree,
            string imageUrl)
        {
            var @event = new Event
            {
                Id = Guid.NewGuid(),
                Title = title,
                Description = description,
                Category = category,
                OrganizerId = organizerId,
                OrganizerName = organizerName,
                Venue = venue,
                TimeRange = timeRange,
                Capacity = capacity,
                TicketPrice = ticketPrice,
                IsFree = isFree,
                ImageUrl = imageUrl,
                Status = EventStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            @event.AddDomainEvent(new EventCreatedDomainEvent(@event.Id, @event.OrganizerId.Value));
            return @event;
        }

        // Domain behaviors
        public void UpdateDetails(
            EventTitle title,
            EventDescription description,
            EventCategory category,
            Venue venue,
            EventTimeRange timeRange,
            Capacity capacity,
            Money ticketPrice,
            bool isFree,
            string imageUrl)
        {
            // Guard clauses and validation
            if (Status == EventStatus.Cancelled)
                throw new InvalidOperationException("Cannot update a cancelled event");

            Title = title;
            Description = description;
            Category = category;
            Venue = venue;
            TimeRange = timeRange;
            Capacity = capacity;
            TicketPrice = ticketPrice;
            IsFree = isFree;
            ImageUrl = imageUrl;
            UpdatedAt = DateTime.UtcNow;

            AddDomainEvent(new EventUpdatedDomainEvent(Id));
        }

        public void UpdateOrganizerName(OrganizerName organizerName)
        {
            OrganizerName = organizerName;
            UpdatedAt = DateTime.UtcNow;
        }

        public void ApproveEvent()
        {
            if (Status != EventStatus.Pending)
                throw new InvalidOperationException("Only pending events can be approved");

            Status = EventStatus.Approved;
            UpdatedAt = DateTime.UtcNow;

            AddDomainEvent(new EventApprovedDomainEvent(Id));
        }

        public void CancelEvent()
        {
            if (Status == EventStatus.Cancelled)
                throw new InvalidOperationException("Event is already cancelled");

            Status = EventStatus.Cancelled;
            UpdatedAt = DateTime.UtcNow;

            AddDomainEvent(new EventCancelledDomainEvent(Id));
        }

        public void ReserveSeats(int numberOfSeats)
        {
            if (Status != EventStatus.Approved)
                throw new InvalidOperationException("Cannot reserve seats for non-approved events");

            if (numberOfSeats > Capacity.AvailableSeats)
                throw new InvalidOperationException("Not enough seats available");

            Capacity = Capacity.ReserveSeats(numberOfSeats);
            UpdatedAt = DateTime.UtcNow;
        }

        public void ReleaseSeats(int numberOfSeats)
        {
            Capacity = Capacity.ReleaseSeats(numberOfSeats);
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
