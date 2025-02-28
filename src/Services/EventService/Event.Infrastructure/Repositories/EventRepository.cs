using Event.Domain.Interfaces;

namespace Event.Infrastructure.Repositories
{
    public class EventRepository : BaseRepository<Event.Domain.Entities.Event>, IEventRepository
    {
        private readonly EventDbContext _context;
        public EventRepository(EventDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
