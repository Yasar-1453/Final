namespace Backend.Api.DTO.User
{
    public class EditUserDto
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public IFormFile ProfilePhoto { get; set; } // Optional: For updating profile photo
    }
}
