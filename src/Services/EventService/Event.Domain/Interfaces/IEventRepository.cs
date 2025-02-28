using Event.Domain.Interfaces.Common;

namespace Event.Domain.Interfaces
{
    public interface IEventRepository : IRepository<Event.Domain.Entities.Event>
    {
    }
}
