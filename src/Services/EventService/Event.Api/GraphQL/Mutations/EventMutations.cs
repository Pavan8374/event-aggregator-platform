using Event.Api.GraphQL.Types;
using Event.Application.Commands.Events.CreateEvent;
using Event.Application.DTOs.Events;
//using HotChocolate.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace Event.Api.GraphQL.Mutations
{
    [Authorize(Roles = ("Admin, Organizer"))]
    [ExtendObjectType("Mutation")]
    public class EventMutations
    {
        private readonly IMediator _mediator;

        public EventMutations(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<EventResponseDto> CreateEventAsync(CreateEventInput input, IFile file)
        {
            var command = new CreateEventCommand(
                input.Title,
                input.Description,
                input.Category,
                input.Venue,
                input.TimeRange,
                input.Capacity,
                input.TicketPrice,
                input.IsFree,
                //input.Thumbnail
                file // File input
            );

            var result = await _mediator.Send(command);
            return result.IsSuccess ? result.Value : throw new GraphQLException(result.Error);
        }
    }
}
