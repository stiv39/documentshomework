namespace Domain.Entities
{
    public class Document
    {
        public Guid Id { get; set; }

        public List<DocumentTag> Tags { get; set; } = new List<DocumentTag>();

        public DocumentData Data { get; set; }
    }
}
