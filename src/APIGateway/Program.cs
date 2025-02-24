using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace ApiGateway;

public class Program
{
    public static async Task<int> Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add configuration files
        builder.Configuration
            .SetBasePath(builder.Environment.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

        // Add Ocelot
        builder.Services.AddOcelot(builder.Configuration);

        builder.WebHost.ConfigureKestrel(options =>
        {
            options.ListenAnyIP(8080, listenOptions =>
            {
                listenOptions.Protocols = HttpProtocols.Http1AndHttp2; // Force HTTP/2
            });
        });

        // Add Swagger
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            // Identity Service Documentation
            c.SwaggerDoc("identity", new OpenApiInfo
            {
                Title = "Identity Service API",
                Version = "v1",
                Description = "Identity management endpoints"
            });

            // Event Service Documentation
            c.SwaggerDoc("events", new OpenApiInfo
            {
                Title = "Event Service API",
                Version = "v1",
                Description = "Event management endpoints"
            });

            // Registration Service Documentation
            c.SwaggerDoc("registration", new OpenApiInfo
            {
                Title = "Registration Service API",
                Version = "v1",
                Description = "Registration management endpoints"
            });
            c.SwaggerDoc("notification", new OpenApiInfo
            {
                Title = "Notification Service API",
                Version = "v1",
                Description = "Notification management endpoints"
            });
            c.SwaggerDoc("analytics", new OpenApiInfo
            {
                Title = "Analytics Service API",
                Version = "v1",
                Description = "Analytics management endpoints"
            });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Description = "JWT Authorization header using the Bearer scheme."
            });
        });


        // Add CORS
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                b => b
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin()
            );
        });

       

        var app = builder.Build();

        app.UseRouting();
        // Enable CORS
        app.UseCors("AllowAll");

        app.Use(async (context, next) =>
        {
            context.Request.EnableBuffering();
            await next();
        });

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            // Add a swagger endpoint for each service
            c.SwaggerEndpoint("/swagger/identity/swagger.json", "Identity Service API");
            c.SwaggerEndpoint("/swagger/events/swagger.json", "Event Service API");
            c.SwaggerEndpoint("/swagger/registration/swagger.json", "Registration Service API");
            c.SwaggerEndpoint("/swagger/notification/swagger.json", "Notification Service API");
            c.SwaggerEndpoint("/swagger/analytics/swagger.json", "Analytics Service API");

            // Customize the Swagger UI
            //c.DocExpansion(DocExpansion.None);
            //c.DefaultModelsExpandDepth(-1); // Hide schemas section
            c.RoutePrefix = "swagger"; // Access Swagger UI at /docs instead of /swagger
        });

        // Configure Ocelot
        await app.UseOcelot();

        await app.RunAsync();
        return 0;
    }
}