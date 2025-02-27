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
                .AddTypeExtension<EventQueries>()
                .AddMutationType(d => d.Name("Mutation"))
                .AddTypeExtension<EventMutations>()
                .AddType<EventType>()
                .AddFiltering()
                .AddSorting()
                .AddProjections()
                //.ModifyPagingOptions(
                //)
                .AddUploadType()  // Add this to support file uploads
                .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true);  // For debugging
             
            return services;
        }
    }
}
