using Application.Dtos;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Models;
using Domain.Repositories;
using Microsoft.Extensions.Logging;
using Moq;


namespace Tests.Services
{
    [TestFixture]
    public class DocumentRepositoryServiceTests
    {
        private DocumentRepositoryService systemUnderTests;
        private Mock<IDocumentRepository> _documentRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<ILogger<DocumentRepositoryService>> _loggerMock;

        [SetUp]
        public void SetUp()
        {
            _documentRepositoryMock = new Mock<IDocumentRepository>();
            _mapperMock = new Mock<IMapper>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _loggerMock = new Mock<ILogger<DocumentRepositoryService>>();


            systemUnderTests = new DocumentRepositoryService(
                _documentRepositoryMock.Object,
                _mapperMock.Object,
                _unitOfWorkMock.Object,
                _loggerMock.Object
            );
        }

        [Test]
        public async Task GetAll_ReturnsAllDocuments()
        {
            // Arrange
            var documents = new List<Document>
         {
            new Document { Id = Guid.NewGuid(), Tags = new List<string> { "Tag1" }, Data = new DocumentData { Name = "Document 1", Content = "Content 1" } },
            new Document { Id = Guid.NewGuid(), Tags = new List<string> { "Tag2" }, Data = new DocumentData { Name = "Document 2", Content = "Content 2" } },
            new Document { Id = Guid.NewGuid(), Tags = new List<string> { "Tag3" }, Data = new DocumentData { Name = "Document 3", Content = "Content 3" } }
        };

            var documentDtos = documents.Select(document => new DocumentDto
            {
                Id = document.Id,
                Tags = document.Tags,
                Data = new DocumentData { Name = document.Data.Name, Content = document.Data.Content }
            });

            _documentRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(documents);
            _mapperMock.Setup(mapper => mapper.Map<DocumentDto>(It.IsAny<Document>()))
                .Returns((Document document) => new DocumentDto
                {
                    Id = document.Id,
                    Tags = document.Tags,
                    Data = new DocumentData { Name = document.Data.Name, Content = document.Data.Content }
                });

            // Act
            var result = await systemUnderTests.GetAll();

            // Assert
            Assert.AreEqual(3, result.Count());
            Assert.IsTrue(result.Any(d => d.Id == documents[0].Id));
            Assert.IsTrue(result.Any(d => d.Id == documents[1].Id));
        }

        [Test]
        public async Task GetById_ExistingId_ReturnsDocumentDto()
        {
            // Arrange
            var documentId = Guid.NewGuid();
            var document = new Document
            {
                Id = documentId,
                Tags = new List<string> { "Tag1" },
                Data = new DocumentData { Name = "Test Document", Content = "Test Content" }
            };

            var documentDto = new DocumentDto
            {
                Id = documentId,
                Tags = document.Tags,
                Data = new DocumentData { Name = document.Data.Name, Content = document.Data.Content }
            };

            _documentRepositoryMock.Setup(repo => repo.GetById(documentId)).ReturnsAsync(document);
            _mapperMock.Setup(mapper => mapper.Map<DocumentDto>(document)).Returns(documentDto);

            // Act
            var result = await systemUnderTests.GetById(documentId);

            // Assert
            Assert.AreEqual(documentDto, result);
        }

        [Test]
        public async Task GetById_NonExistingId_ReturnsNull()
        {
            // Arrange
            var documentId = Guid.NewGuid();

            _documentRepositoryMock.Setup(repo => repo.GetById(documentId)).ReturnsAsync((Document)null);

            // Act
            var result = await systemUnderTests.GetById(documentId);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void Add_ValidDocument_ReturnsNewlyCreatedId()
        {
            // Arrange
            var documentDto = new CreateDocumentDto
            {
                Tags = new List<string> { "Tag1", "Tag2" },
                Data = new DocumentData { Name = "New Document", Content = "New Content" }
            };

            var document = new Document
            {
                Id = Guid.NewGuid(),
                Tags = documentDto.Tags,
                Data = new DocumentData { Name = documentDto.Data.Name, Content = documentDto.Data.Content }
            };

            var newId = document.Id;

            _mapperMock.Setup(mapper => mapper.Map<Document>(documentDto)).Returns(document);
            _documentRepositoryMock.Setup(repo => repo.GetCreatedOrUpdatedEntity(document)).Returns(document);

            // Act
            var result = systemUnderTests.Add(documentDto);

            // Assert
            Assert.AreEqual(newId, result);
        }

        [Test]
        public void Update_ValidDocument_ReturnsUpdatedDocument()
        {
            // Arrange
            var documentDto = new DocumentDto
            {
                Id = Guid.NewGuid(),
                Tags = new List<string> { "Tag1", "Tag2" },
                Data = new DocumentData { Name = "Updated Document", Content = "Updated Content" }
            };

            var document = new Document
            {
                Id = documentDto.Id,
                Tags = documentDto.Tags,
                Data = new DocumentData { Name = documentDto.Data.Name, Content = documentDto.Data.Content }
            };

            
            _mapperMock.Setup(mapper => mapper.Map<Document>(documentDto)).Returns(document);
            _documentRepositoryMock.Setup(repo => repo.Update(It.IsAny<Document>()));
            _unitOfWorkMock.Setup(uow => uow.SaveChanges()).Verifiable();
            _documentRepositoryMock.Setup(repo => repo.GetCreatedOrUpdatedEntity(It.IsAny<Document>())).Returns(document);

            // Act
            var result = systemUnderTests.Update(documentDto);

            // Assert
            _unitOfWorkMock.Verify(uow => uow.SaveChanges(), Times.Once);
        }

        [Test]
        public void Update_ExceptionOccurs_ReturnsNull()
        {
            // Arrange
            var documentDto = new DocumentDto
            {
                Id = Guid.NewGuid(),
                Tags = new List<string> { "Tag1", "Tag2" },
                Data = new DocumentData { Name = "Updated Document", Content = "Updated Content" }
            };

            var document = new Document
            {
                Id = documentDto.Id,
                Tags = documentDto.Tags,
                Data = new DocumentData { Name = documentDto.Data.Name, Content = documentDto.Data.Content }
            };

            _mapperMock.Setup(mapper => mapper.Map<Document>(documentDto)).Returns(document);
            _documentRepositoryMock.Setup(repo => repo.Update(document));
            _unitOfWorkMock.Setup(uow => uow.SaveChanges()).Throws(new Exception());

            // Act
            var result = systemUnderTests.Update(documentDto);

            // Assert
            Assert.IsNull(result);
            _unitOfWorkMock.Verify(uow => uow.SaveChanges(), Times.Once);
        }
    }
}

