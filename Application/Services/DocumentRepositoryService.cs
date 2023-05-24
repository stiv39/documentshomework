﻿using Application.Dtos;
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

        public async Task<Guid?> Add(CreateDocumentDto documentDto)
        {
            var mapped = _mapper.Map<Document>(documentDto);

            _documentRepository.Add(mapped);
            await _unitOfWork.SaveChangesAsync();

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

                docFromDb.Data = new DocumentData { Content = documentDto.Data.Content, Name = documentDto.Data.Name};
                docFromDb.Tags = GetDocumentTags(documentDto);

                _documentRepository.Update(docFromDb);
                await _unitOfWork.SaveChangesAsync();

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

        private List<DocumentTag> GetDocumentTags(DocumentDto dto)
        {
            var tagsList = new List<DocumentTag>();

            foreach (var tag in dto.Tags)
            {
                tagsList.Add(new DocumentTag { Name = tag.Name });
            }

            return tagsList;
        }
    }
}
