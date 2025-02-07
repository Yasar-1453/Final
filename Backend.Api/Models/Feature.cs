using Backend.Api.Models.Common;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Backend.Api.Models
{
    public class Feature : BaseEntity
    {
        public string Name { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public List<Game>? Games { get; set; }
    }
}
