using AutoMapper;
using Backend.Api.DTO.User;
using Backend.Api.Helpers.Exceptions;
using Backend.Api.Models;
using Backend.Api.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Backend.Api.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        readonly IConfiguration _config;
        readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public UserService(UserManager<AppUser> userManager, IMapper mapper, IConfiguration config, IWebHostEnvironment env)
        {
            _userManager = userManager;
            _mapper = mapper;
            _config = config;
            _env = env;
        }

        public async Task<string> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Username);

            if (user == null) throw new UserLoginException();


            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!result) throw new UserLoginException();

            var _claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("Name", user.Name),


            };

            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Security"]));
            SigningCredentials _signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken jwtToken = new JwtSecurityToken(
                audience: _config["Jwt:Audience"],
                issuer: _config["Jwt:Issuer"],
                claims: _claims,
                signingCredentials: _signingCredentials,
                expires: DateTime.UtcNow.AddMinutes(60)
                );
            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return token;

        }

        public async Task Register(RegisterDto dto)
        {
            if (await _userManager.FindByEmailAsync(dto.Email) != null)
            {
                throw new UserRegisterException("vardir bir bele mail - master yoda");
            }

            var appUser = _mapper.Map<AppUser>(dto);

            if (dto.ProfilePhoto != null && dto.ProfilePhoto.Length > 0)
            {
                string photoPath = await SaveProfilePhotoAsync(dto.ProfilePhoto);
                appUser.ProfilePhotoPath = photoPath;
            }

            var result = await _userManager.CreateAsync(appUser, dto.Password);

            if (!result.Succeeded)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var ex in result.Errors)
                {


                    sb.AppendLine(ex.Description + " ");


                    throw new UserRegisterException(sb.ToString());
                }
            }
        }

        public async Task<string> UpdateProfilePhotoAsync(string userId, IFormFile profilePhoto)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) throw new Exception("User not found.");

            user.ProfilePhotoPath = await SaveProfilePhotoAsync(profilePhoto);
            await _userManager.UpdateAsync(user);

            return user.ProfilePhotoPath;
        }

        public async Task<AppUser> EditUserAsync(string userId, EditUserDto dto)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) throw new Exception("User not found.");

            user.UserName = dto.Username ?? user.UserName;
            user.Name = dto.Name ?? user.Name;
            user.Email = dto.Email ?? user.Email;

            if (dto.ProfilePhoto != null)
            {
                user.ProfilePhotoPath = await SaveProfilePhotoAsync(dto.ProfilePhoto);
            }

            await _userManager.UpdateAsync(user);
            return user;
        }

        public async Task<AppUser> GetUserByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId) ?? throw new Exception("User not found.");
        }

        private async Task<string> SaveProfilePhotoAsync(IFormFile profilePhoto)
        {
            var uploadsFolder = Path.Combine(_env.WebRootPath, "profile-photos");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var fileName = Guid.NewGuid() + Path.GetExtension(profilePhoto.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await profilePhoto.CopyToAsync(stream);

            return $"/profile-photos/{fileName}";
        }


    }
}
