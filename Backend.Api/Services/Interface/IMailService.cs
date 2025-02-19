using Backend.Api.Helpers.Email;

namespace Backend.Api.Services.Interface
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);

    }
}
