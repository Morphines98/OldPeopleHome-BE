using AutoMapper;
using MeerPflege.Application.DTOs;
using MeerPflege.Domain;

namespace MeerPflege.Application.Core
{
  public class MappingProfiles : Profile
  {
    public MappingProfiles()
    {
      CreateMap<HomeGroup, HomeGroup>();
      CreateMap<NewsItem, NewsItemDto>().ReverseMap();
      CreateMap<NewsItemAttachment, NewsItemAttachmentDto>().ReverseMap();
      CreateMap<Nurse, NurseDto>().ReverseMap();
    }
  }
}