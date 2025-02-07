using AutoMapper;
using Backend.Api.DTO.Game;
using Backend.Api.Models;

namespace Backend.Api.Helpers.Mappers
{
    public class GameMapper : Profile
    {
        public GameMapper()
        {
            CreateMap<CreateGameDto, Game>().ReverseMap();
            CreateMap<GetGameDto, Game>().ReverseMap();
            CreateMap<GetAllGameDto, Game>().ReverseMap();
            CreateMap<UpdateGameDto, GetGameDto>().ReverseMap();
        }
    }
}
