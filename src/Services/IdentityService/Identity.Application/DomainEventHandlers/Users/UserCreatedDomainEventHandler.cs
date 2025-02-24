using Identity.Domain.Events.Users;
using Identity.Domain.Interfaces.Common;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Identity.Application.DomainEventHandlers.Users
{
    public class UserCreatedDomainEventHandler : INotificationHandler<UserCreatedEvent>
    {
        private readonly ILogger<UserCreatedDomainEventHandler> _logger;
        //private readonly IEmailService _emailService;
        private readonly IExecutionContextAccessor _executionContext;

        public UserCreatedDomainEventHandler(
            ILogger<UserCreatedDomainEventHandler> logger,
            //IEmailService emailService,
            IExecutionContextAccessor executionContext)
        {
            _logger = logger;
            //_emailService = emailService;
            _executionContext = executionContext;
        }

        public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Handling UserCreated domain event. UserId: {UserId}, CorrelationId: {CorrelationId}",
                notification.UserId,
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
                    "Error handling UserCreated domain event. UserId: {UserId}, CorrelationId: {CorrelationId}",
                    notification.UserId,
                    _executionContext.CorrelationId);
                throw;
            }
        }
    }
}
