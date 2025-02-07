using Backend.Api.Models.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Backend.Api.Repositories.Interface
{
    public interface IRepository<TEntity> where TEntity : BaseEntity, new()
    {
        public DbSet<TEntity> Table { get; }
        public Task<TEntity> GetById(int id);
        public IQueryable<TEntity> GetAll();
        public Task<TEntity> Create(TEntity entity);
        public void Update(TEntity entity);
        public void Delete(TEntity entity);
        public void SoftDelete(TEntity entity);
        public Task<int> SaveChangesAsync();
        public Task<bool> IsExsist(Expression<Func<TEntity, bool>> expression);
    }
}
