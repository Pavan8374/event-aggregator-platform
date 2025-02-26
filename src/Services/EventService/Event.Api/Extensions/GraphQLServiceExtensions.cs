using Event.Api.GraphQL.Mutations;
using Event.Api.GraphQL.Types;
using Event.GraphQL.Queries;

namespace Event.Api.Extensions
{
    public static class GraphQLServiceExtensions
    {
        public static IServiceCollection AddGraphQLServices(this IServiceCollection services)
        {
            services
                .AddGraphQLServer()
                .AddQueryType(d => d.Name("Query"))
                .AddType<EventQueries>()       // Register Queries
                .AddMutationType<EventMutations>() // Register Mutations
                .AddType<CreateEventInput>()   // Register Input Types
                .AddType<EventType>()
                .AddFiltering()
                .AddSorting()
                // Add these lines for the playground and schema explorer
                //.AddBananaCakePop()           // Adds Banana Cake Pop UI (HotChocolate's playground)
                .AddInMemorySubscriptions();   // Add if you plan to use subscriptions
            return services;
        }
    }
}
