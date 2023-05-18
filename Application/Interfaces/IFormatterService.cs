using Application.Dtos;
using Domain.Models;

namespace Application.Interfaces
{
    public interface IFormatterService
    {
        FormattedData FormatDocument(DocumentDto document, string format);
    }
}
