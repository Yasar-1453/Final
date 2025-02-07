using Backend.Api.Models.Common;
using System.Text.Json.Serialization;

namespace Backend.Api.Models
{
    public class Genre : BaseEntity
    {
        public string Name { get; set; }
        [JsonIgnore]
        public List<Game> Games { get; set; }
    }
}
