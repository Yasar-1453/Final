namespace Backend.Api.DTO.GameKey
{
    public class CreateGameKeyDto
    {
        public string Key { get; set; } // Activation key

        public bool IsSold { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        public int GameId { get; set; }
    }
}
