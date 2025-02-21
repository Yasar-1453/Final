namespace Backend.Api.Models
{
    public class Basket
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ICollection<BasketItem> Items { get; set; }

        public decimal TotalPrice => Items.Sum(item => item.Game.Price * item.Quantity);
        public int TotalItemsCount => Items.Sum(item => item.Quantity);
    }
}

