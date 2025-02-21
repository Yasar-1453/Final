using Backend.Api.Models;

namespace Backend.Api.Services.Interface
{
    public interface IBasketService
    {
        Task<Basket> GetBasketAsync(string userId);
        Task<Basket> AddGameToBasketAsync(string userId, int gameId, int quantity);
        Task<Basket> RemoveGameFromBasketAsync(string userId, int gameId);
        Task ClearBasketAsync(string userId);
    }
}
