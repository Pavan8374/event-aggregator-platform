using Event.Domain.Events.Events;
using Event.Domain.Interfaces.Common;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Event.Application.DomainEventHandlers.Events
{
    public class EventCreatedDomainEventHandler : INotificationHandler<EventCreatedDomainEvent>
    {
        private readonly ILogger<EventCreatedDomainEventHandler> _logger;
        //private readonly IEmailService _emailService;
        private readonly IExecutionContextAccessor _executionContext;

        public EventCreatedDomainEventHandler(ILogger<EventCreatedDomainEventHandler> logger,
            //IEmailService emailService,
            IExecutionContextAccessor executionContext)
        {
            _logger = logger;
            //_emailService = emailService;
            _executionContext = executionContext;
        }
        public async Task Handle(EventCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                 $"Handling EventCreated domain event. EventId: {notification.EventId}, CorrelationId: {_executionContext.CorrelationId}",
                 notification.EventId,
                 _executionContext.CorrelationId);

            try
            {
                // Send welcome email
                //await _emailService.SendWelcomeEmailAsync(notification.Email);

                // Additional processing...
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    $"Error handling EventCreated domain event. UserId: {notification.EventId}, CorrelationId: {_executionContext.CorrelationId}",
                    notification.OrganizerId,
                    _executionContext.CorrelationId);
                throw;
            }
        }
    }
}
