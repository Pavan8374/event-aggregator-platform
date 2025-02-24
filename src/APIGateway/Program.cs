using Microsoft.AspNetCore.Server.Kestrel.Core;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

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

        // Configure Ocelot
        await app.UseOcelot();

        await app.RunAsync();
        return 0;
    }
}