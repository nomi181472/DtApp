using System.Linq;
using AutoMapper;
using DatingApp.API.Dtos;
using DatingApp.API.Models;

namespace DatingApp.API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserForListDto>().ForMember(dist => dist.PhotoUrl, opt =>
            {
                opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
            })
            .ForMember(dist => dist.Age, opt => { opt.ResolveUsing(d => d.DateOfBirt.CalculateAge()); });
            CreateMap<User, UserForDetailedDto>().ForMember(dist => dist.PhotoUrl, opt =>
           {
               opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
           }).ForMember(dist => dist.Age, opt => { opt.ResolveUsing(d => d.DateOfBirt.CalculateAge()); });
            CreateMap<Photo, PhotosForDetailsDto>();

        }
    }
}