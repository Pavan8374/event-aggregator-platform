using Event.Api.Extensions;
using Event.Api.Services.Interfaces;
using Event.Infrastructure.Extensions;
using Event.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();
builder.Services.AddControllers();

builder.Services.AddOpenApi();
builder.Services.AddSwaggerService();
builder.Services.AddCors();

// Register Infrastructure Services
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.Load("Event.Application")));
builder.Services.AddHttpContextAccessor();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();

// Register services
builder.Services.AddScoped<IEventService, EEventService>();

// Add GraphQL services
builder.Services.AddGraphQLServices();

var app = builder.Build();
app.UseRouting();
app.UseCors(options =>
{
    options
        .WithOrigins(builder.Configuration.GetValue<string>("AllowedHosts"))
        .AllowAnyHeader()
        .AllowAnyMethod();
});
app.MapOpenApi();
app.UseSwagger(); // Load Swagger before auth
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapGraphQL("/graphql");
app.Run();
