using Domain.Entities;

namespace Domain.Repositories
{
    public interface IDocumentRepository
    {
        Task<IEnumerable<Document>> GetAll(int pageNumber, int pageSize);

        Task<Document?> GetById(Guid id);

        Task Add(Document entity);

        void Update(Document entity);

        Document? GetCreatedOrUpdatedEntity(Document entity);
    }
}
