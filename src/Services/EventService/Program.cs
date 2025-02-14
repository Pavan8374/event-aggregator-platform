using EventService.GraphQL.Queries;
using EventService.GraphQL.Types;
using EventService.Services;
using EventService.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);


// Register services
builder.Services.AddScoped<IEventService, EEventService>();

// Add GraphQL services
builder.Services
    .AddGraphQLServer()
    .AddQueryType(d => d.Name("Query"))
    .AddType<EventQueries>()
    .AddType<EventType>()
    .AddFiltering()
    .AddSorting();

var app = builder.Build();

app.UseRouting();
app.MapGraphQL("/graphql");

app.Run();
