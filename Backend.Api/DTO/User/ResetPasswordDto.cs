using System.ComponentModel.DataAnnotations;

namespace Backend.Api.DTO.User
{
    public class ResetPasswordDto
    {
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [DataType(DataType.Password), Compare(nameof(NewPassword))]
        public string ConfirmNewPassword { get; set; }
        public string token { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
    }
}
