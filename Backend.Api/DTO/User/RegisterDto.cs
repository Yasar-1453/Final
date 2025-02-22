using FluentValidation;
using System.Text.RegularExpressions;

namespace Backend.Api.DTO.User
{
    public class RegisterDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public IFormFile? ProfilePhoto { get; set; }
    }

    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(u => u.Name)
                .NotEmpty()
                .NotNull();
            RuleFor(u => u.UserName)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(50);
            RuleFor(u => u.Email)
                .NotEmpty()
                .NotNull()
                .Must(x =>
                {
                    Regex regex = new Regex("^[a-z0-9](\\.?[a-z0-9]){5,}@g(oogle)?mail\\.com$");
                    var r = regex.Match(x);
                    return r.Success;
                });
            RuleFor(u => u.Password)
                .NotEmpty()
                .NotNull()
                .MinimumLength(4);
            RuleFor(u => u)
                .NotEmpty()
                .NotNull()
                .Must(u => u.Password == u.ConfirmPassword);
        }
    }
}
