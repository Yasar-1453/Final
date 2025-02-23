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
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(UserManager<AppUser> userManager, IMapper mapper, IConfiguration config, IWebHostEnvironment env, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _config = config;
            _env = env;
            _roleManager = roleManager;
        }

        public async Task<string> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Username);

            if (user == null) throw new UserLoginException();


            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!result) throw new UserLoginException();

            var userRoles = await _userManager.GetRolesAsync(user);

            var _claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("Name", user.Name),


            };

            _claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

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

        public async Task Register(RegisterDto dto, string profilePhotoUrl)
        {
            string roleName = dto.Role.ToString();
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }
            if (await _userManager.FindByEmailAsync(dto.Email) != null)
            {
                throw new UserRegisterException("vardir bir bele mail - master yoda");
            }

            var appUser = _mapper.Map<AppUser>(dto);


            if (dto.ProfilePhoto != null && dto.ProfilePhoto.Length > 0)
            {
                var photoPath = await SaveProfilePhotoAsync(dto.ProfilePhoto);
              
             
                appUser.ProfilePhotoPath = $"{profilePhotoUrl}{photoPath}";
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

            await _userManager.AddToRoleAsync(appUser, roleName);
        }

 

        public async Task<AppUser> EditUserAsync(string userId, EditUserDto dto, string profilePhotoUrl)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) throw new Exception("User not found.");

            user.UserName = dto.Username ?? user.UserName;
            user.Name = dto.Name ?? user.Name;
            user.Email = dto.Email ?? user.Email;

            if (dto.ProfilePhoto != null && dto.ProfilePhoto.Length > 0)
            {
                var photoPath = await SaveProfilePhotoAsync(dto.ProfilePhoto);


                user.ProfilePhotoPath = $"{profilePhotoUrl}{photoPath}";
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
            if (profilePhoto == null || profilePhoto.Length == 0)
                throw new Exception("Invalid image file.");

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(profilePhoto.FileName);
            var filePath = Path.Combine(_env.WebRootPath, "User", "Profile", fileName);

            // Ensure the directory exists
            if (!Directory.Exists(Path.Combine(_env.WebRootPath, "User", "Profile")))
            {
                Directory.CreateDirectory(Path.Combine(_env.WebRootPath, "User", "Profile"));
            }

            // Save the file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await profilePhoto.CopyToAsync(stream);
            }


            return $"/User/Profile/{fileName}";
        }
        public async Task<List<string>> GetUserRoles(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }

    }
}
