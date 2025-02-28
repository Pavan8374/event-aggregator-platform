using Event.Api.Models;

namespace Event.Api.GraphQL.Types
{
    public class EventType : ObjectType<Event.Api.Models.Event>
    {
        protected override void Configure(IObjectTypeDescriptor<Event.Api.Models.Event> descriptor)
        {
            descriptor.Description("Represents an event in the system");

            descriptor.Field(e => e.Id)
                .Description("The unique identifier of the event");

            descriptor.Field(e => e.Name)
                .Description("The name of the event");

            descriptor.Field(e => e.Description)
                .Description("The description of the event");

            descriptor.Field(e => e.StartDate)
                .Description("When the event starts");

            descriptor.Field(e => e.EndDate)
                .Description("When the event ends");

            descriptor.Field(e => e.Location)
                .Description("Where the event takes place");

            descriptor.Field(e => e.Attendees)
                .Description("List of attendees for this event");
        }
    }
}
