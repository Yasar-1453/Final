using Backend.Api.DAL;
using Backend.Api.Models;
using Backend.Api.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Backend.Api.Services.Implementations
{
    public class BasketService : IBasketService
    {
        private readonly AppDbContext _context;

        public BasketService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Basket> GetBasketAsync(string userId)
        {
            var basket = await _context.Baskets
                .Include(b => b.Items)
                .ThenInclude(i => i.Game)
                .FirstOrDefaultAsync(b => b.UserId == userId);

            if (basket == null)
            {
                basket = new Basket { UserId = userId };
                _context.Baskets.Add(basket);
                await _context.SaveChangesAsync();
            }

            return basket;
        }

        public async Task<Basket> AddGameToBasketAsync(string userId, int gameId, int quantity)
        {
            var basket = await GetBasketAsync(userId);
            var game = await _context.Games.FindAsync(gameId);

            if (game == null)
            {
                throw new Exception("Game not found.");
            }

            var existingItem = basket.Items.FirstOrDefault(i => i.GameId == gameId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                basket.Items.Add(new BasketItem
                {
                    GameId = gameId,
                    Quantity = quantity
                });
            }

            await _context.SaveChangesAsync();
            return basket;
        }

        public async Task<Basket> RemoveGameFromBasketAsync(string userId, int gameId)
        {
            var basket = await GetBasketAsync(userId);
            var itemToRemove = basket.Items.FirstOrDefault(i => i.GameId == gameId);

            if (itemToRemove != null)
            {
                basket.Items.Remove(itemToRemove);
                _context.BasketItems.Remove(itemToRemove);
                await _context.SaveChangesAsync();
            }

            return basket;
        }

        public async Task ClearBasketAsync(string userId)
        {
            var basket = await GetBasketAsync(userId);
            _context.BasketItems.RemoveRange(basket.Items);
            basket.Items.Clear();
            await _context.SaveChangesAsync();
        }
    }
}
