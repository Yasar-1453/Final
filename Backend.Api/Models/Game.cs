using Backend.Api.Models.Common;
using System.Text.Json.Serialization;

namespace Backend.Api.Models
{
    public class Game : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountedPrice { get; set; }
        public DateTime ReleaseDate { get; set; } = DateTime.UtcNow;
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }

        public int GenreId { get; set; }
        [JsonIgnore]
        public Genre Genre { get; set; }
        public int FeaturesId { get; set; }
        [JsonIgnore]
        public Feature Features { get; set; }
        [JsonIgnore]
        public ICollection<GameKey> GameKeys { get; set; }
       


    }
}
