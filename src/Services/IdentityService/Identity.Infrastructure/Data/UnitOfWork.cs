using Identity.Domain.Common;
using Identity.Domain.Events.Common;
using Identity.Domain.Interfaces.Common;
using Microsoft.EntityFrameworkCore.Storage;

namespace Identity.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IdentityDbContext _context;
        private readonly IDomainEventService _domainEventService;
        private IDbContextTransaction _currentTransaction;

        public UnitOfWork(
            IdentityDbContext context,
            IDomainEventService domainEventService)
        {
            _context = context;
            _domainEventService = domainEventService;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            // Dispatch Domain Events before saving
            await DispatchDomainEventsAsync();

            // Save changes to the database
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task BeginTransactionAsync()
        {
            if (_currentTransaction != null)
            {
                return;
            }

            _currentTransaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                await _currentTransaction?.CommitAsync()!;
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public async Task RollbackTransactionAsync()
        {
            try
            {
                await _currentTransaction?.RollbackAsync()!;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        private async Task DispatchDomainEventsAsync()
        {
            var domainEntities = _context.ChangeTracker
                .Entries<AggregateRoot>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any())
                .ToList();

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
            {
                await _domainEventService.PublishAsync(domainEvent);
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
