using Backend.Api.DTO.Feature;
using Backend.Api.DTO.Game;

namespace Backend.Api.Services.Interface
{
    public interface IGameService
    {
        Task<GetGameDto> CreateAsync(CreateGameDto dto, string imageUrl);
        Task<GetGameDto> GetById(int id);
        List<GetGameDto> GetAll();
        Task Update(UpdateGameDto dto);
        Task Delete(int id);
        Task SoftDelete(int id);
    }
}
