using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace Persistence.Repositories
{
    public class CachedDocumentRepository : IDocumentRepository
    {
        private readonly IDocumentRepository _decorated;
        private readonly IMemoryCache _memoryCache;

        public CachedDocumentRepository(IDocumentRepository decorated, IMemoryCache memoryCache)
        {
            _decorated = decorated;
            _memoryCache = memoryCache;
        }

        public async Task<IEnumerable<Document>> GetAll(int pageNumber, int pageSize)
        {
            var documents = await _decorated.GetAll(pageNumber, pageSize);
            return documents;
        }

        public Task<Document?> GetById(Guid id)
        {
            string key = $"member-{id}";

            return _memoryCache.GetOrCreateAsync(
                key,
                entry =>
                {
                    entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));

                    return _decorated.GetById(id);
                });
        }
        public async Task Add(Document entity)
        {
            await _decorated.Add(entity);
        }

        public void Update(Document entity)
        {
            _decorated.Update(entity);
        }

        public Document? GetCreatedOrUpdatedEntity(Document entity)
        {
            return _decorated.GetCreatedOrUpdatedEntity(entity);
        }

        public async Task<int> CountAll()
        {
            return await _decorated.CountAll();
        }
    }
}
