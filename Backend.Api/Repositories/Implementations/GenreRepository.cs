using Backend.Api.DAL;
using Backend.Api.Models;
using Backend.Api.Repositories.Interface;

namespace Backend.Api.Repositories.Implementations
{
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        public GenreRepository(AppDbContext context) : base(context)
        {
        }
    }
}
