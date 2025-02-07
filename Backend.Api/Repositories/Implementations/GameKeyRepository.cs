using Backend.Api.DAL;
using Backend.Api.Models;
using Backend.Api.Repositories.Interface;

namespace Backend.Api.Repositories.Implementations
{
    public class GameKeyRepository : Repository<GameKey>, IGameKeyRepository
    {
        public GameKeyRepository(AppDbContext context) : base(context)
        {
        }
    }
}
