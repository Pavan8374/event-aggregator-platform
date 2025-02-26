using Event.Application.DTOs.Events;
using Event.Domain.Enumerations;
using Event.Domain.ValueObjects.Events;
using MediatR;

namespace Event.Application.Commands.Events.CreateEvent
{
    public class CreateEventCommand : IRequest<Domain.Common.Result<EventResponseDto>>
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public EventCategory Category { get; private set; }
        public string Venue { get; private set; }
        public EventTimeRange TimeRange { get; private set; }
        public Capacity Capacity { get; private set; }
        public Money TicketPrice { get; private set; }
        public bool IsFree { get; private set; }

        [GraphQLNonNullType]
        public IFile Thumbnail { get; private set; }

        public CreateEventCommand(
            string title, string description, EventCategory category,
            string venue, EventTimeRange timeRange, Capacity capacity,
            Money ticketPrice, bool isFree, IFile thumbnail)
        {
            Title = title;
            Description = description;
            Category = category;
            Venue = venue;
            TimeRange = timeRange;
            Capacity = capacity;
            TicketPrice = ticketPrice;
            IsFree = isFree;
            Thumbnail = thumbnail;
        }
    }
}
