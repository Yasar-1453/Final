using AutoMapper;
using Backend.Api.DTO.GameKey;
using Backend.Api.Models;

namespace Backend.Api.Helpers.Mappers
{
    public class GameKeyMapper : Profile
    {
        public GameKeyMapper()
        {
            CreateMap<CreateGameKeyDto, GameKey>().ReverseMap();
            CreateMap<GetGameKeyDto, GameKey>().ReverseMap();
            CreateMap<GetAllGameKeyDto, GameKey>().ReverseMap();
            CreateMap<UpdateGameKeyDto, GetGameKeyDto>().ReverseMap();

        }
    }
}
