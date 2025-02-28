using Event.Application.DTOs.Events;
using Event.Domain.Common;
using Event.Domain.Interfaces;
using Event.Domain.Interfaces.Common;
using Event.Domain.Services;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Event.Application.Commands.Events.CreateEvent
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Domain.Common.Result<EventResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IEventRepository _eventRepository;
        private readonly IExecutionContextAccessor _executionContextAccessor;
        private readonly IFileStorageService _fileStorageService;

        public CreateEventCommandHandler(
            IUnitOfWork unitOfWork,
            IConfiguration configuration, 
            IEventRepository eventRepository,
            IExecutionContextAccessor executionContextAccessor,
            IFileStorageService fileStorageService)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _eventRepository = eventRepository;
            _executionContextAccessor = executionContextAccessor;
            _fileStorageService = fileStorageService;
        }
        public async Task<Domain.Common.Result<EventResponseDto>> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Make sure you await all async operations and pass the cancellation token
                string imageURL = "";
                if (request.Thumbnail?.Length > 0)
                {
                    using Stream fileStream = request.Thumbnail.OpenReadStream();
                    Guid organizerId = _executionContextAccessor.UserId;
                    string folderPath = $"event-management/organizers/{organizerId}/";
                    imageURL = await _fileStorageService.UploadFileAsync(fileStream, request.Thumbnail.Name, folderPath);
                }

                var newEvent = Event.Domain.Entities.Event.Create(
                    request.Title,
                    request.Description,
                    request.Category,
                    _executionContextAccessor.UserId,
                    _executionContextAccessor.UserName,
                    request.Venue,
                    request.TimeRange,
                    request.Capacity,
                    request.TicketPrice,
                    request.IsFree,
                    imageURL
                );

                await _eventRepository.AddAsync(newEvent);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result.Success(new EventResponseDto() { Id = newEvent.Id });
            }
            catch (DomainException ex)
            {
                return Result.Failure<EventResponseDto>(ex.Message);
            }
        }
    }
}
