using Domain.Models;

namespace Application.Dtos
{
    public class CreateDocumentDto
    {
        public List<string> Tags { get; set; } = new List<string>();

        public DocumentData Data { get; set; }
    }
}
