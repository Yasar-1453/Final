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

        public UserService(UserManager<AppUser> userManager, IMapper mapper, IConfiguration config)
        {
            _userManager = userManager;
            _mapper = mapper;
            _config = config;
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
    }
}
