using Identity.Domain.Common;
using Identity.Domain.Interfaces.Common;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : AggregateRoot
    {
        protected readonly IdentityDbContext _context;

        protected BaseRepository(IdentityDbContext context)
        {
            _context = context;
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public virtual void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
    }
}
