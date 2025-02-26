﻿using Microsoft.AspNetCore.Identity;
using System.Net.Http.Headers;

namespace Backend.Api.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string ProfilePhotoPath { get; set; }
        public string Role {  get; set; }
    }
}
