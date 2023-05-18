using Application.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Models;

namespace Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DocumentDto, MsgPackDocument>().ReverseMap();
            CreateMap<Document, DocumentDto>()
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
              .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags))
              .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data));

            CreateMap<DocumentDto, Document>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags))
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data));

            CreateMap<CreateDocumentDto, Document>()
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags))
                .ReverseMap();
        }
    }
}
