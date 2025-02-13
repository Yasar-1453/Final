using Backend.Api.DTO.User;

namespace Backend.Api.Services.Interface
{
    public interface IUserService
    {
        Task Register(RegisterDto dto);
        Task<string> Login(LoginDto dto);
    }
}
