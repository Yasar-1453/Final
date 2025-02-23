using Backend.Api.DTO.User;
using Backend.Api.Models;

namespace Backend.Api.Services.Interface
{
    public interface IUserService
    {
        Task Register(RegisterDto dto, string profilePhotoUrl);
        Task<string> Login(LoginDto dto);
        Task<AppUser> EditUserAsync(string userId, EditUserDto dto, string profilePhotoUrl);
        Task<List<string>> GetUserRoles(string username);
        Task<AppUser> GetUserByIdAsync(string userId);
    }
}
