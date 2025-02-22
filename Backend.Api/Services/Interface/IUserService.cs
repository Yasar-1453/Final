using Backend.Api.DTO.User;
using Backend.Api.Models;

namespace Backend.Api.Services.Interface
{
    public interface IUserService
    {
        Task Register(RegisterDto dto);
        Task<string> Login(LoginDto dto);
        Task<string> UpdateProfilePhotoAsync(string userId, IFormFile profilePhoto);
        Task<AppUser> EditUserAsync(string userId, EditUserDto dto);
        Task<AppUser> GetUserByIdAsync(string userId);
    }
}
