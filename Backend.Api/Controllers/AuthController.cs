using Backend.Api.DTO.User;
using Backend.Api.Helpers.Email;
using Backend.Api.Models;
using Backend.Api.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Security.Claims;

namespace Backend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly IUserService _userService;
        readonly UserManager<AppUser> _userManager;
        readonly IMailService _mailService;

        public AuthController(IUserService userService, UserManager<AppUser> userManager, IMailService mailService)
        {
            _userService = userService;
            _userManager = userManager;
            _mailService = mailService;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            try
            {
                string profilePhotoUrl = $"{Request.Scheme}://{Request.Host}"; 

          
                await _userService.Register(dto, profilePhotoUrl);
             
                return Ok();
              
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            try
            {
                return Ok(await _userService.Login(dto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto dto)
        {
            if(!ModelState.IsValid) return BadRequest();

            AppUser user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null) return NotFound();
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            object userStat = new
            {
                userId = user.Email,
                token = token
            };
            var link = $"http://localhost:5173/resetPassword";
            MailRequest mailRequest = new MailRequest()
            {
                ToEmail = dto.Email,
                Subject = "Reset Password",
                Body = $"<a href='{link}'> Reset Password</a>"
            };

            await _mailService.SendEmailAsync(mailRequest);

            return Ok(new
            {
                token = token,
                email = user.Email,
            });
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.email) || string.IsNullOrWhiteSpace(dto.token) || string.IsNullOrWhiteSpace(dto.NewPassword))
            {
                return BadRequest(new { message = "All fields are required." });
            }

            var user = await _userManager.FindByEmailAsync(dto.email);
            if (user == null)
            {
                return BadRequest(new { message = "User not found." });
            }

            var trimmedPassword = dto.NewPassword.Trim();


            var decodedToken = Uri.UnescapeDataString(dto.token);

            var result = await _userManager.ResetPasswordAsync(user, decodedToken, trimmedPassword);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return BadRequest(new { message = "Password reset failed.", errors });
            }

            return Ok(new { message = "Password has been reset successfully." });
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(string userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            return Ok(user);
        }

        [HttpPut("{userId}/edit")]
        public async Task<IActionResult> EditUser(string userId, EditUserDto dto)
        {
            string profilePhotoUrl = $"{Request.Scheme}://{Request.Host}";
            var updatedUser = await _userService.EditUserAsync(userId, dto, profilePhotoUrl);
            return Ok(updatedUser);
        }

        [HttpGet("my-roles")]
        [Authorize]
        public IActionResult GetMyRoles()
        {
            var roles = User.Claims
                            .Where(c => c.Type == ClaimTypes.Role)
                            .Select(c => c.Value)
                            .ToList();

            return Ok(new { roles });
        }

        // ✅ Get Roles for Any User (Admin Only)
        [HttpGet("{username}/roles")]
        [Authorize(Roles = "Admin")] // Only Admins can check other users' roles
        public async Task<IActionResult> GetUserRoles(string username)
        {
            var roles = await _userService.GetUserRoles(username);
            return Ok(new { username, roles });
        }
    }
}
