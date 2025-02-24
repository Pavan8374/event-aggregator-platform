using Microsoft.OpenApi.Models;

namespace ApiGateway
{
    public class OcelotSwaggerGenerator
    {
        private readonly IConfiguration _configuration;

        public OcelotSwaggerGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public OpenApiDocument GenerateDocument(string name, OpenApiInfo info)
        {
            var routes = _configuration.GetSection("Routes").Get<List<OcelotRouteConfig>>();
            var document = new OpenApiDocument
            {
                Info = info,
                Paths = new OpenApiPaths(),
                Components = new OpenApiComponents()
            };

            foreach (var route in routes.Where(r => IsRouteForService(r, name)))
            {
                var path = new OpenApiPathItem();
                foreach (var method in route.UpstreamHttpMethod)
                {
                    var operation = new OpenApiOperation
                    {
                        Description = $"Proxied to {route.DownstreamHostAndPorts.First().Host}:{route.DownstreamHostAndPorts.First().Port}",
                        Tags = new List<OpenApiTag> { new OpenApiTag { Name = name } }
                    };

                    path.Operations.Add(GetOperationType(method), operation);
                }

                document.Paths.Add(route.UpstreamPathTemplate, path);
            }

            return document;
        }

        private bool IsRouteForService(OcelotRouteConfig route, string serviceName)
        {
            return route.DownstreamHostAndPorts.Any(h =>
                h.Host.ToLower().Contains(serviceName.ToLower()));
        }

        private OperationType GetOperationType(string method)
        {
            return method.ToUpper() switch
            {
                "GET" => OperationType.Get,
                "POST" => OperationType.Post,
                "PUT" => OperationType.Put,
                "DELETE" => OperationType.Delete,
                "OPTIONS" => OperationType.Options,
                _ => OperationType.Get
            };
        }
    }

    public class OcelotRouteConfig
    {
        public string DownstreamPathTemplate { get; set; }
        public string UpstreamPathTemplate { get; set; }
        public List<string> UpstreamHttpMethod { get; set; }
        public List<OcelotHostAndPort> DownstreamHostAndPorts { get; set; }
    }

    public class OcelotHostAndPort
    {
        public string Host { get; set; }
        public int Port { get; set; }
    }
}
