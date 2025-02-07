using Backend.Api.DTO.Game;
using Backend.Api.DTO.GameKey;

namespace Backend.Api.Services.Interface
{
    public interface IGameKeyService
    {
        Task<GetGameKeyDto> CreateAsync(CreateGameKeyDto dto);
        Task<GetGameKeyDto> GetById(int id);
        List<GetGameKeyDto> GetAll();
        Task Update(UpdateGameKeyDto dto);
        Task Delete(int id);
        Task SoftDelete(int id);
    }
}
