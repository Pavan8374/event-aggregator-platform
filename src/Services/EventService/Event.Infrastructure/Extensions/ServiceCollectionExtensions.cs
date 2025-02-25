using Event.Domain.Events.Common;
using Event.Domain.Interfaces.Common;
using Event.Infrastructure.Context;
using Event.Infrastructure.Data;
using Event.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Event.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDomainEventService, DomainEventService>();
            services.AddScoped<IExecutionContextAccessor, ExecutionContextAccessor>();

            var connection = configuration.GetConnectionString("DefaultConnectionString");
            services.AddDbContext<EventDbContext>(options =>
                options.UseNpgsql(connection, o => o.EnableRetryOnFailure(3))
                       .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));


            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
