using AutoMapper;
using MarvelApp.Application.DTOs;
using MarvelApp.Domain.Entities;

namespace MarvelApp.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Favorite, ComicDto>();
        }
    }
}
