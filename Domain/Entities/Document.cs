using Domain.Models;

namespace Domain.Entities
{
    public class Document
    {
        public Guid Id { get; set; }

        public List<string> Tags { get; set; } = new List<string>();

        public DocumentData Data { get; set; }
    }
}
