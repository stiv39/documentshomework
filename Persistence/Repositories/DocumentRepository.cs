using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly DataContext _dataContext;

        public DocumentRepository(DataContext dataContext) =>
            _dataContext = dataContext;

        public async Task<IEnumerable<Document>> GetAll(int pageNumber, int pageSize)
        {
            var skip = (pageNumber - 1) * pageSize;

            var documents = await _dataContext.Documents
                .Skip(skip)
                .Take(pageSize)
                .Include(d => d.Tags)
                .Include(d => d.Data)
                .ToListAsync();

            return documents;
        }
        public async Task<Document?> GetById(Guid id)
        {
            var document = await _dataContext.Documents
                .Include(d => d.Tags)
                .Include(d => d.Data)
                .FirstOrDefaultAsync(d => d.Id == id);

            return document;
        }
        public async Task Add(Document entity)
        {
            await _dataContext.Documents.AddAsync(entity);
        }
        public void Update(Document entity)
        {
            _dataContext.Documents.Update(entity);
        }

        public Document? GetCreatedOrUpdatedEntity(Document entity)
        {
            var newEntityEntry = _dataContext.Entry(entity);

            return newEntityEntry.Entity;
        }
    }
}
