using Identity.Domain.Events.Common;
using Identity.Domain.Interfaces;
using Identity.Domain.Interfaces.Common;
using Identity.Infrastructure.Context;
using Identity.Infrastructure.Data;
using Identity.Infrastructure.Repositories;
using Identity.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDomainEventService, DomainEventService>();
            services.AddScoped<IExecutionContextAccessor, ExecutionContextAccessor>();

            var connection = configuration.GetConnectionString("DefaultConnectionString");
            services.AddDbContext<IdentityDbContext>(options =>
                options.UseNpgsql(connection, o => o.EnableRetryOnFailure(3))
                       .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));


            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
