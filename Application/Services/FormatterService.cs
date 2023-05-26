using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using MessagePack;
using System.IO;
using System.Net.Mime;
using System.Xml.Serialization;

namespace Application.Services
{
    public class FormatterService : IFormatterService
    {
        private readonly IMapper _mapper;

        public FormatterService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public FormattedData FormatDocument(DocumentDto document, string format)
        {
            switch (format.ToLower())
            {
                case "application/x-msgpack": return new FormattedData() { Format = "application/x-msgpack", Data = MsgPackFormatter(_mapper.Map<MsgPackDocument>(document)) };
                case MediaTypeNames.Application.Xml:
                default: return new FormattedData() { Format = MediaTypeNames.Application.Xml, Data = XmlFormatter(document) };
            }
        }

        private byte[] MsgPackFormatter(MsgPackDocument document)
        {
            byte[] msgPackData = MessagePackSerializer.Serialize(document);
            return msgPackData;
        }

        private byte[] XmlFormatter(DocumentDto document)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(DocumentDto));
            byte[] xmlData;

            using (MemoryStream ms = new MemoryStream())
            {
                serializer.Serialize(ms, document);
                xmlData = ms.ToArray();
            }
            
            return xmlData;
        }
    }
}
