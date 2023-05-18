using Domain.Models;

namespace Application.Dtos
{
    public class DocumentDto
    {
        public Guid Id { get; set; }
        public List<string> Tags { get; set; } = new List<string>();

        public DocumentData Data { get; set; }
    }
}
