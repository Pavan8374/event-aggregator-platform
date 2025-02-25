using Event.Domain.Interfaces.Common;
using Microsoft.AspNetCore.Http;

namespace Event.Infrastructure.Context
{
    public class ExecutionContextAccessor : IExecutionContextAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ExecutionContextAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid UserId
        {
            get
            {
                if (_httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated == true)
                {
                    var claim = _httpContextAccessor.HttpContext.User.Claims
                        .FirstOrDefault(x => x.Type == "sub");
                    if (claim != null && Guid.TryParse(claim.Value, out Guid userId))
                    {
                        return userId;
                    }
                }
                return Guid.Empty;
            }
        }

        public string UserName
        {
            get
            {
                return _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? string.Empty;
            }
        }

        public string CorrelationId
        {
            get
            {
                return _httpContextAccessor.HttpContext?.TraceIdentifier ?? Guid.NewGuid().ToString();
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
            }
        }
    }
}
