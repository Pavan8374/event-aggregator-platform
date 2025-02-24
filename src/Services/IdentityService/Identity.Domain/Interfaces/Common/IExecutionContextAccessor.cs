namespace Identity.Domain.Interfaces.Common
{
    public interface IExecutionContextAccessor
    {
        Guid UserId { get; }
        string UserName { get; }
        string CorrelationId { get; }
        bool IsAuthenticated { get; }
    }
}
