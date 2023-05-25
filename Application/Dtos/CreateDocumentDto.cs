namespace Application.Dtos
{
    public class CreateDocumentDto
    {
        public List<DocumentTagDto> Tags { get; set; } = new List<DocumentTagDto>();

        public DocumentDataDto Data { get; set; }
    }
}
