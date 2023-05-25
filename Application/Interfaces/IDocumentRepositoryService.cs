using Application.Dtos;
using Application.Models;

namespace Application.Interfaces
{
    public interface IDocumentRepositoryService
    {
        Task<DocumentsResponse> GetAll(int pageNumber, int pageSize);

        Task<DocumentDto?> GetById(Guid id);

        Task<Guid?> Add(CreateDocumentDto entity);

        Task<DocumentDto?> Update(DocumentDto entity);
    }
}
