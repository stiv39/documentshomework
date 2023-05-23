using Application.Dtos;

namespace Application.Interfaces
{
    public interface IDocumentRepositoryService
    {
        Task<IEnumerable<DocumentDto>> GetAll();

        Task<DocumentDto?> GetById(Guid id);

        Guid? Add(CreateDocumentDto entity);

        Task<DocumentDto?> Update(DocumentDto entity);
    }
}
