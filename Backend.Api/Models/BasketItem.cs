using System.Text.Json.Serialization;

namespace Backend.Api.Models
{
    public class BasketItem
    {
        public int Id { get; set; }
        public int BasketId { get; set; }
        [JsonIgnore]
        public Basket Basket { get; set; }

        public int GameId { get; set; }
        [JsonIgnore]
        public Game Game { get; set; }

        public int Quantity { get; set; }
    }
}
