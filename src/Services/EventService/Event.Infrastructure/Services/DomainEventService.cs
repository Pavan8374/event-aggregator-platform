using Event.Domain.Common;
using Event.Domain.Events.Common;
using Event.Domain.Interfaces.Common;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Event.Infrastructure.Services
{
    public class DomainEventService : IDomainEventService
    {
        private readonly IMediator _mediator;
        private readonly ILogger<DomainEventService> _logger;
        private readonly IExecutionContextAccessor _executionContextAccessor;

        public DomainEventService(
            IMediator mediator,
            ILogger<DomainEventService> logger,
            IExecutionContextAccessor executionContextAccessor)
        {
            _mediator = mediator;
            _logger = logger;
            _executionContextAccessor = executionContextAccessor;
        }

        public async Task PublishAsync(DomainEvent domainEvent)
        {
            var eventType = domainEvent.GetType().Name;
            var correlationId = _executionContextAccessor.CorrelationId;

            try
            {
                _logger.LogInformation(
                    $"Publishing domain event. Event: {eventType} CorrelationId: {correlationId}",
                    eventType,
                    correlationId);

                await _mediator.Publish(domainEvent);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    $"Error publishing domain event: {eventType} CorrelationId: {correlationId}",
                    eventType,
                    correlationId);
                throw;
            }
        }

        public async Task PublishAsync(IEnumerable<DomainEvent> domainEvents)
        {
            foreach (var domainEvent in domainEvents)
            {
                await PublishAsync(domainEvent);
            }
        }
    }
}
