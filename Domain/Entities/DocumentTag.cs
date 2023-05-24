namespace Domain.Entities
{
    public class DocumentTag
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Document Document { get; set; }
        public Guid DocumentId { get; set; }
    }
}
