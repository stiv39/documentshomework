using Application.Dtos;

namespace Application.Interfaces
{
    public interface IDocumentRepositoryService
    {
        Task<IEnumerable<DocumentDto>> GetAll(int pageNumber, int pageSize);

        Task<DocumentDto?> GetById(Guid id);

        Task<Guid?> Add(CreateDocumentDto entity);

        Task<DocumentDto?> Update(DocumentDto entity);
    }
}
