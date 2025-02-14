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
            options.ListenAnyIP(8000, listenOptions =>
            {
                listenOptions.Protocols = HttpProtocols.Http2; // Force HTTP/2
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

        // Configure Ocelot
        await app.UseOcelot();

        await app.RunAsync();
        return 0;
    }
}