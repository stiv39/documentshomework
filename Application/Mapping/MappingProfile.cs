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
            CreateMap<DocumentDataDto, MsgPackDocumentData>().ReverseMap();

            CreateMap<Document, DocumentDto>()
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
              .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags));

            CreateMap<DocumentDto, Document>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags));

            CreateMap<CreateDocumentDto, Document>()
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags))
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data));

            CreateMap<DocumentData, DocumentDataDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
                .ReverseMap();

            CreateMap<DocumentTag, DocumentTagDto>()
                 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                 .ReverseMap();
        }
    }
}
