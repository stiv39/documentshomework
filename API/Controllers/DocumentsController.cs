using Application.Dtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentRepositoryService _documentRepositoryService;
        private readonly IFormatterService _formatterService;
        private readonly ILogger _logger;

        public DocumentsController(IDocumentRepositoryService documentRepositoryService, IFormatterService formatterService, ILogger<DocumentsController> logger)
        {
            _documentRepositoryService = documentRepositoryService;
            _formatterService = formatterService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<DocumentDto>))]
        public async Task<ActionResult<IEnumerable<DocumentDto>>> GetAllDocuments(int pageNumber = 1, int pageSize = 100)
        {
            try
            {
                if (pageNumber <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page number must be greater than zero.");
                }

                var documents = await _documentRepositoryService.GetAll(pageNumber, pageSize);

                return Ok(documents);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get all documents", ex);
                throw;
            }
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DocumentDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/xml", "application/json", "application/x-msgpack")]
        public async Task<ActionResult<DocumentDto>> GetDocumentById(Guid id, [FromHeader(Name = "Accept")] string acceptHeader = MediaTypeNames.Application.Json)
        {
            try
            {
                var document = await _documentRepositoryService.GetById(id);

                if (document == null)
                {
                    return NotFound();
                }

                if (acceptHeader.ToLower() == MediaTypeNames.Application.Json)
                {
                    return Ok(document);
                }

                var result = _formatterService.FormatDocument(document, acceptHeader);
                return new FileContentResult(result.Data, result.Format);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get document by id {id}", ex);
                throw;
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> SaveDocument([FromBody] CreateDocumentDto documentDto)
        {
            try
            {
                var id = await _documentRepositoryService.Add(documentDto);
                if (id == null) { return BadRequest("Failed to create"); }

                return Ok(id);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to create", ex);
                throw;
            }

        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DocumentDto>> UpdateDocument([FromBody] DocumentDto documentDto)
        {
            try
            {
                var result = await _documentRepositoryService.Update(documentDto);

                if (result == null)
                {
                    return BadRequest("Failed to update");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to update", ex);
                throw;
            }
        }
    }
}
