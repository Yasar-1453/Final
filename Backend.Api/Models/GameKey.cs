using Backend.Api.Models.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Backend.Api.Models
{
    public class GameKey : BaseEntity
    {
        public string Key { get; set; } // Activation key
        public long Price {  get; set; }

        public bool IsSold { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        public int GameId { get; set; }
        [JsonIgnore]
        public Game? Game { get; set; }
    }
}
