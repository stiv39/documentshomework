namespace Application.Dtos
{
    public class DocumentDto
    {
        public Guid Id { get; set; }
        public List<DocumentTagDto> Tags { get; set; } = new List<DocumentTagDto>();

        public DocumentDataDto Data { get; set; }
    }
}
