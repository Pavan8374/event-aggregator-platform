using EventService.GraphQL.Queries;
using EventService.GraphQL.Types;
using EventService.Services;
using EventService.Services.Interfaces;
using Microsoft.OpenApi.Models;

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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Event Service API",
        Version = "v1",
        Description = "Event Service"
    });
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Event  Service API V1");
        c.RoutePrefix = "docs";
    });
}

app.UseRouting();
app.MapGraphQL("/graphql");

app.Run();
