namespace Domain.Entities
{
    public class DocumentData
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public Document Document { get; set; }
        public Guid DocumentId { get; set; }
    }
}
