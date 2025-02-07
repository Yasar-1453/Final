using Backend.Api.DAL;
using Backend.Api.Models;
using Backend.Api.Repositories.Interface;

namespace Backend.Api.Repositories.Implementations
{
    public class GameRepository : Repository<Game>, IGameRepository
    {
        public GameRepository(AppDbContext context) : base(context)
        {
        }
    }
}
