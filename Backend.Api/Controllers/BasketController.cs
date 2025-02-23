using Backend.Api.DTO.Basket;
using Backend.Api.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetBasket(string userId)
        {
            var basket = await _basketService.GetBasketAsync(userId);
            return Ok(new
            {
                basket.Id,
                basket.UserId,
                TotalPrice = basket.TotalPrice,
                TotalItemsCount = basket.TotalItemsCount,
                Items = basket.Items.Select(item => new
                {
                    item.Id,
                    item.GameId,
                    item.Game.Name,
                    item.Game.Price,
                    item.Quantity
                })
            });
        }

        [HttpPost("{userId}/add")]
        public async Task<IActionResult> AddGame(string userId, [FromBody] AddGameDto dto)
        {
            var basket = await _basketService.AddGameToBasketAsync(userId, dto.GameId, dto.Quantity);
            return Ok(new
            {
                message = "Game added to basket",
                TotalItemsCount = basket.TotalItemsCount,
                TotalPrice = basket.TotalPrice
            });
        }

        [HttpDelete("{userId}/remove/{gameId}")]
        public async Task<IActionResult> RemoveGame(string userId, int gameId)
        {
            var basket = await _basketService.RemoveGameFromBasketAsync(userId, gameId);
            return Ok(new
            {
                message = "Game removed from basket",
                TotalItemsCount = basket.TotalItemsCount,
                TotalPrice = basket.TotalPrice
            });
        }

        [HttpDelete("{userId}/clear")]
        public async Task<IActionResult> ClearBasket(string userId)
        {
            await _basketService.ClearBasketAsync(userId);
            return Ok(new { message = "Basket cleared successfully", TotalItemsCount = 0, TotalPrice = 0 });
        }
    }
}
