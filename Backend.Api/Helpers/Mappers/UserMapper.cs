using AutoMapper;
using Backend.Api.DTO.User;
using Backend.Api.Models;

namespace Backend.Api.Helpers.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<RegisterDto, AppUser>().ReverseMap();
        }
    }
}
