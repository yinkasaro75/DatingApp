using System.Linq;
using AutoMapper;
using DatingApp.API.Dto;
using DatingApp.API.Models;

namespace DatingApp.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User,UserForDetail>()
            .ForMember(des => des.PhotoUrl, opt => {
                opt.MapFrom(src => src.Photos.FirstOrDefault(u => u.IsMain).Url);
            }).ForMember(des => des.Age, opt => {
                opt.ResolveUsing(src => src.DateofBirth.CalculateAge());
            });
            
         CreateMap<User,UserForList>()
           .ForMember(des => des.PhotoUrl, opt => {
                opt.MapFrom(src => src.Photos.FirstOrDefault(u => u.IsMain).Url);
            }).ForMember(des => des.Age, opt => {
                opt.ResolveUsing(src => src.DateofBirth.CalculateAge());
            });
            CreateMap<Photo,PhotosForDetailedDto>();
        }
    }
}