using AutoMapper;
using Backend.Api.DTO.Genre;
using Backend.Api.Models;

namespace Backend.Api.Helpers.Mappers
{
    public class GenreMapper : Profile
    {
        public GenreMapper()
        {
            CreateMap<CreateGenreDto, Genre>().ReverseMap();
            CreateMap<GetGenreDto, Genre>().ReverseMap();
            CreateMap<GetAllGenreDto, Genre>().ReverseMap();
            CreateMap<UpdateGenreDto, GetGenreDto>().ReverseMap();
        }
    }
}
