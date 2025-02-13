using Microsoft.AspNetCore.Identity;
using System.Net.Http.Headers;

namespace Backend.Api.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
