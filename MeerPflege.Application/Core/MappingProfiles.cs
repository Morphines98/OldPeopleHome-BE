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
      CreateMap<Carer, CarersDto>().ReverseMap();
      CreateMap<Activity, ActivityDto>().ReverseMap();
      CreateMap<Elder, ElderDto>().ReverseMap();
      CreateMap<WallItem, WallItemDto>().ReverseMap();
      CreateMap<WallItemAttachment, WallItemAttachmentDto>().ReverseMap();
      CreateMap<ActivityElderPresence, ActivityElderDto>();
      CreateMap<ActivityElderDto,ActivityElderPresence>()
      .ForMember(dest => dest.Elder,opt =>opt.Ignore())
      .ForMember(dest => dest.Activity,opt =>opt.Ignore());
    }
  }
}