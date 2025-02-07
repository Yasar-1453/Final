using Backend.Api.DAL;
using Backend.Api.Models;
using Backend.Api.Repositories.Interface;

namespace Backend.Api.Repositories.Implementations
{
    public class FeaturesRepository : Repository<Feature>, IFeaturesRepository
    {
        public FeaturesRepository(AppDbContext context) : base(context)
        {
        }
    }
}
