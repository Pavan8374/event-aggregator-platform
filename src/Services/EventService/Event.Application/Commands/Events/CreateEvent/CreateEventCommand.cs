using Event.Application.DTOs.Events;
using Event.Domain.Common;
using Event.Domain.Enumerations;
using Event.Domain.ValueObjects.Events;
using MediatR;

namespace Event.Application.Commands.Events.CreateEvent
{
    public class CreateEventCommand : IRequest<Result<EventResponseDto>>
    {
        //Need to modify
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
    }
}
