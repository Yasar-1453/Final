using FluentValidation;

namespace Backend.Api.DTO.User
{
    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginDtoValidoator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidoator()
        {
            RuleFor(u => u.Username)
                .NotNull()
                .NotEmpty();
            RuleFor(u => u.Password)
                .NotNull()
                .NotEmpty()
                .MinimumLength(6)
                .WithMessage("Username ve ya password sehvdir");
        }
    }
}
