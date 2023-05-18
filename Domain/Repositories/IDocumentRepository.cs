using Domain.Entities;

namespace Domain.Repositories
{
    public interface IDocumentRepository
    {
        Task<IEnumerable<Document>> GetAll();

        Task<Document?> GetById(Guid id);

        void Add(Document entity);

        void Update(Document entity);

        Document? GetCreatedOrUpdatedEntity(Document entity);
    }
}
