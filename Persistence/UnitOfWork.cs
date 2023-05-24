using Domain.Interfaces;

namespace Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dbContext;

        public UnitOfWork(DataContext dbContext) => _dbContext = dbContext;

        public Task SaveChangesAsync() =>
            _dbContext.SaveChangesAsync();
    }
}
