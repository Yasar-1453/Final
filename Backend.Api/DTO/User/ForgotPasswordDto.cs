using System.ComponentModel.DataAnnotations;

namespace Backend.Api.DTO.User
{
    public class ForgotPasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
