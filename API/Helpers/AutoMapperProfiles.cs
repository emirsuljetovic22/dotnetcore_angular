using API.DTOs;
using API.Entities;
using AutoMapper;
using API.Entities.Articles;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Tag, TagDto>().ReverseMap();
            CreateMap<Tag, TagBDto>().ReverseMap();
            CreateMap<AppUser, MemberDto>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url));
            CreateMap<Photo, PhotoDto>();
            CreateMap<MemberUpdateDto, AppUser>();
            CreateMap<RegisterDto, AppUser>();
            CreateMap<Message, MessageDto>()
                .ForMember(
                dest => dest.SenderPhotoUrl, 
                opt => opt.MapFrom(src => src.Sender.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(
                dest => dest.RecipientPhotoUrl, 
                opt => opt.MapFrom(src => src.Recipient.Photos.FirstOrDefault(x => x.IsMain).Url));
            CreateMap<Article, ArticleListDto>()
                .ForMember(
                dest => dest.Category,
                opt => opt.MapFrom(src => src.ArticleCategories.FirstOrDefault().CategoryName));
            CreateMap<Article, ArticleDto>()
                .ForMember(
                dest => dest.Category,
                opt => opt.MapFrom(src => src.ArticleCategories.FirstOrDefault().CategoryName))
                .ForMember(
                dest => dest.CategoryId,
                opt => opt.MapFrom(src => src.ArticleCategories.FirstOrDefault().Id)).ReverseMap();
            CreateMap<Article, ArticleAddUpdateDto>().ReverseMap();
            CreateMap<Article, ArticleOnlyDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryToReturnDto>().ReverseMap();
            CreateMap<Category, CategoryToUpdateDto>().ReverseMap();
        }
    }
}
