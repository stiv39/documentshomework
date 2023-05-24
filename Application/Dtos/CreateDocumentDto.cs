namespace Application.Dtos
{
    public class CreateDocumentDto
    {
        public List<string> Tags { get; set; } = new List<string>();

        public DocumentDataDto Data { get; set; }
    }
}
