namespace Backend.Api.DTO.GameKey
{
    public class UpdateGameKeyDto
    {
        public int Id { get; set; }
        public string Key { get; set; } // Activation key

        public bool IsSold { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        public int GameId { get; set; }
    }
}
