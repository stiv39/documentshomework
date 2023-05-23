using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class DocumentRepositoryService : IDocumentRepositoryService
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public DocumentRepositoryService(
            IDocumentRepository documentRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ILogger<DocumentRepositoryService> logger)
        {
            _documentRepository = documentRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IEnumerable<DocumentDto>> GetAll()
        {
            var documents = await _documentRepository.GetAll();
            var documentDtos = documents.Select(document => _mapper.Map<DocumentDto>(document));
            return documentDtos;
        }

        public async Task<DocumentDto?> GetById(Guid id)
        {
            var document = await _documentRepository.GetById(id);
            if (document == null) { return null; }

            return _mapper.Map<DocumentDto>(document);
        }

        public Guid? Add(CreateDocumentDto documentDto)
        {
            var mapped = _mapper.Map<Document>(documentDto);
            _documentRepository.Add(mapped);
            _unitOfWork.SaveChanges();

            var entity = _documentRepository.GetCreatedOrUpdatedEntity(mapped);

            return entity?.Id;
        }

        public async Task<DocumentDto?> Update(DocumentDto documentDto)
        {
            try
            {
                var docFromDb = await _documentRepository.GetById(documentDto.Id);

                if(docFromDb == null)
                {
                    return null;
                }

                docFromDb.Data = documentDto.Data;
                docFromDb.Tags = documentDto.Tags;

                _documentRepository.Update(docFromDb);
                _unitOfWork.SaveChanges();

                var updated = _documentRepository.GetCreatedOrUpdatedEntity(docFromDb);

                if(updated == null)
                {
                    return null;
                }
                return _mapper.Map<DocumentDto>(updated);
            }
            catch (Exception ex)
            {
                _logger.LogError("update exception", ex);
                return null;
            }
        }
    }
}
