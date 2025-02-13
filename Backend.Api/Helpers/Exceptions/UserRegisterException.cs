namespace Backend.Api.Helpers.Exceptions
{
    public class UserRegisterException : Exception
    {
        public UserRegisterException() : base("gerizekali duzgun register ol") { }


        public UserRegisterException(string? message) : base(message)
        {
        }
    }
}
