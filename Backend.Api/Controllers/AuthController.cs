using Backend.Api.DTO.User;
using Backend.Api.Models;
using Backend.Api.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace Backend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly IUserService _userService;
        readonly UserManager<AppUser> _userManager;

        public AuthController(IUserService userService, UserManager<AppUser> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            try
            {
                await _userService.Register(dto);
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
            var link = Url.Action("ResetPassword", "Auth", userStat,HttpContext.Request.Scheme);

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

            // ✅ Trim password to remove hidden spaces
            var trimmedPassword = dto.NewPassword.Trim();

            // ✅ URL-decode the token
            var decodedToken = Uri.UnescapeDataString(dto.token);

            // ✅ Reset password
            var result = await _userManager.ResetPasswordAsync(user, decodedToken, trimmedPassword);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return BadRequest(new { message = "Password reset failed.", errors });
            }

            return Ok(new { message = "Password has been reset successfully." });
        }
    }
}
