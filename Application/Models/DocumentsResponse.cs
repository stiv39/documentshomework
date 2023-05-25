using Application.Dtos;

namespace Application.Models
{
    public class DocumentsResponse
    {
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public int PageNumber { get; set; }
        public IEnumerable<DocumentDto> Documents { get; set; } = new List<DocumentDto>();
    }
}
