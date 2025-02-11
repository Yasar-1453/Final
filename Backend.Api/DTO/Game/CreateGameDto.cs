﻿using Backend.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace Backend.Api.DTO.Game
{
    public class CreateGameDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountedPrice { get; set; }
        public DateTime ReleaseDate { get; set; } = DateTime.UtcNow;
        public string? ImageUrl { get; set; } 

        [Required]
        public IFormFile? Image { get; set; } 
        public bool IsActive { get; set; }

        public int GenreId { get; set; }
      
        public int FeaturesId { get; set; }
      

    }
}
