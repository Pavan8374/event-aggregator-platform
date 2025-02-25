using Identity.Domain.Common;

namespace Identity.Domain.Interfaces.Common
{
    public interface IRepository<T> where T : AggregateRoot
    {
        Task<T> GetByIdAsync(Guid id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
