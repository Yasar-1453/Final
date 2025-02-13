namespace Backend.Api.Helpers.Exceptions
{
    public class UserLoginException : Exception
    {
        public UserLoginException() : base("zehmet olmasa omega3 dermani gebul edin")
        {
        }

        public UserLoginException(string? message) : base(message)
        {
        }
    }
}
