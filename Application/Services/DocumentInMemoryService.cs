using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;


namespace Application.Services
{
    public class DocumentInMemoryService : IDocumentInMemoryService
    {
        private readonly IMapper _mapper;

        private List<Document> _documents = new();

        public DocumentInMemoryService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public DocumentDto? GetDocumentById(Guid id)
        {
            var doc = _documents.FirstOrDefault(d => d.Id == id);
            if (doc == null) return null;

            return _mapper.Map<DocumentDto>(doc);
        }

        public IEnumerable<DocumentDto> GetDocuments()
        {
            return _documents.Select(document => _mapper.Map<DocumentDto>(document));
        }

        public Guid SaveDocument(CreateDocumentDto documentdto)
        {
            var id = Guid.NewGuid();
           // _documents.Add(new Document { Id = id, Data = documentdto.Data, Tags = documentdto.Tags });
            return id;
        }

        public bool UpdateDocument(DocumentDto documentDto)
        {
            var originalDocument = _documents.FirstOrDefault(d => d.Id == documentDto.Id);
            if (originalDocument != null)
            {
                // originalDocument.Tags = documentDto.Tags;
                //originalDocument.Data = documentDto.Data;
                return true;
            }
            return false;
        }
    }
}
