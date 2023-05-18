using Application.Dtos;

namespace Application.Interfaces
{
    public interface IDocumentInMemoryService
    {
        public IEnumerable<DocumentDto> GetDocuments();
        public DocumentDto? GetDocumentById(Guid id);
        public Guid SaveDocument(CreateDocumentDto documentdto);
        public bool UpdateDocument(DocumentDto document);
    }
}
