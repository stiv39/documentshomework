﻿using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly DataContext _dataContext;

        public DocumentRepository(DataContext dataContext) =>
            _dataContext = dataContext;

        public async Task<IEnumerable<Document>> GetAll()
        {
            var documents = await _dataContext.Documents.ToListAsync();
            return documents;
        }
        public async Task<Document?> GetById(Guid id)
        {
            var document = await _dataContext.Documents.FindAsync(id);
            return document;
        }
        public void Add(Document entity)
        {
            _dataContext.Documents.Add(entity);
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
