namespace Backend.Api.DTO.Game
{
    public class GetGameDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountedPrice { get; set; }
        public DateTime ReleaseDate { get; set; } = DateTime.UtcNow;
        public string? ImageUrl { get; set; } // ✅ Single Image URL
        public List<string>? ImageUrls { get; set; }

        public bool IsActive { get; set; }

        public int GenreId { get; set; }

        public int FeaturesId { get; set; }
    }
}
