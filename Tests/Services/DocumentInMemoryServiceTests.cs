//using Application.Dtos;
//using Application.Mapping;
//using Application.Services;
//using AutoMapper;
//using Domain.Entities;
//using Domain.Models;

//namespace Tests.Services
//{
//    [TestFixture]
//    public class DocumentInMemoryServiceTests
//    {
//        private DocumentInMemoryService _documentService;
//        private IMapper _mapper;

//        [SetUp]
//        public void Setup()
//        {
//            var mapperConfig = new MapperConfiguration(cfg =>
//            {
//                cfg.AddProfile(new MappingProfile());
//            });

//            _mapper = mapperConfig.CreateMapper();

//            _documentService = new DocumentInMemoryService(_mapper);
//        }

//        [Test]
//        public void GetDocumentById_ExistingId_ReturnsDocumentDto()
//        {
//            // Arrange
//            var documentDto = new CreateDocumentDto
//            {
//                Tags = new List<string> { "Tag1" },
//                Data = new DocumentData { Name = "Document 1", Content = "Content 1" }
//            };
//            var documentId = _documentService.SaveDocument(documentDto);

//            // Act
//            var result = _documentService.GetDocumentById(documentId);

//            // Assert
//            Assert.NotNull(result);
//            Assert.AreEqual(documentId, result.Id);
//            Assert.AreEqual(documentDto.Tags, result.Tags);
//            Assert.AreEqual(documentDto.Data.Name, result.Data.Name);
//            Assert.AreEqual(documentDto.Data.Content, result.Data.Content);
//        }

//        [Test]
//        public void GetDocumentById_NonExistingId_ReturnsNull()
//        {
//            // Arrange
//            var documentId = Guid.NewGuid();

//            // Act
//            var result = _documentService.GetDocumentById(documentId);

//            // Assert
//            Assert.Null(result);
//        }

//        [Test]
//        public void GetDocuments_ReturnsAllDocuments()
//        {
//            // Arrange
//            var documents = new List<Document>
//            {
//                new Document { Id = Guid.NewGuid(), Tags = new List<string> { "Tag1" }, Data = new DocumentData { Name = "Document 1", Content = "Content 1" } },
//                new Document { Id = Guid.NewGuid(), Tags = new List<string> { "Tag2" }, Data = new DocumentData { Name = "Document 2", Content = "Content 2" } },
//                new Document { Id = Guid.NewGuid(), Tags = new List<string> { "Tag3" }, Data = new DocumentData { Name = "Document 3", Content = "Content 3" } }
//            };
//            foreach (var document in documents)
//            {
//                _documentService.SaveDocument(_mapper.Map<CreateDocumentDto>(document));
//            }

//            // Act
//            var result = _documentService.GetDocuments();

//            // Assert
//            Assert.NotNull(result);
//            Assert.AreEqual(documents.Count, result.Count());
//        }

//        [Test]
//        public void SaveDocument_ValidDocument_ReturnsNewlyCreatedId()
//        {
//            // Arrange
//            var documentDto = new CreateDocumentDto
//            {
//                Tags = new List<string> { "Tag1", "Tag2" },
//                Data = new DocumentData { Name = "New Document", Content = "New Content" }
//            };

//            // Act
//            var result = _documentService.SaveDocument(documentDto);

//            // Assert
//            Assert.AreNotEqual(Guid.Empty, result);
//        }

//        [Test]
//        public void UpdateDocument_ExistingDocument_ReturnsTrue()
//        {
//            // Arrange
//            var documentDto = new CreateDocumentDto
//            {
//                Tags = new List<string> { "Tag1" },
//                Data = new DocumentData { Name = "Document 1", Content = "Content 1" }
//            };
//            var documentId = _documentService.SaveDocument(documentDto);
//            var updatedDocumentDto = new DocumentDto
//            {
//                Id = documentId,
//                Tags = new List<string> { "Tag1", "Tag2", "Tag3" },
//                Data = new DocumentData { Name = "Updated Document", Content = "Updated Content" }
//            };

//            // Act
//            var result = _documentService.UpdateDocument(updatedDocumentDto);
//            var updatedDocument = _documentService.GetDocumentById(documentId);

//            // Assert
//            Assert.True(result);
//            Assert.NotNull(updatedDocument);
//            Assert.AreEqual(updatedDocumentDto.Tags, updatedDocument.Tags);
//            Assert.AreEqual(updatedDocumentDto.Data.Name, updatedDocument.Data.Name);
//            Assert.AreEqual(updatedDocumentDto.Data.Content, updatedDocument.Data.Content);
//        }

//        [Test]
//        public void UpdateDocument_NonExistingDocument_ReturnsFalse()
//        {
//            // Arrange
//            var documentDto = new DocumentDto
//            {
//                Id = Guid.NewGuid(),
//                Tags = new List<string> { "Tag1", "Tag2" },
//                Data = new DocumentData { Name = "Document", Content = "Content" }
//            };

//            // Act
//            var result = _documentService.UpdateDocument(documentDto);

//            // Assert
//            Assert.False(result);
//            ADD NEW ASSERTS SOMETHING LIKE THIS
//            Assert.NotNull(updatedDocument);
//            Assert.AreEqual(updatedDocumentDto.Tags, updatedDocument.Tags);
//            Assert.AreEqual(updatedDocumentDto.Data.Name, updatedDocument.Data.Name);
//            Assert.AreEqual(updatedDocumentDto.Data.Content, updatedDocument.Data.Content);
//        }
//    }
//}
