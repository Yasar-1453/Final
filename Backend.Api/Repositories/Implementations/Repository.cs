using Backend.Api.DAL;
using Backend.Api.Models.Common;
using Backend.Api.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Backend.Api.Repositories.Implementations
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity, new()
    {
        private AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public DbSet<TEntity> Table => _context.Set<TEntity>();

        public async Task<TEntity> Create(TEntity entity)
        {
            await Table.AddAsync(entity);
            return entity;
        }

        public void Delete(TEntity entity)
        {
            Table.Remove(entity);
        }

   

        public IQueryable<TEntity> GetAll()
        {
            return Table;
        }

        public async Task<TEntity> GetById(int id)
        {
            return await Table.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<bool> IsExsist(Expression<Func<TEntity, bool>> expression)
        {
            return Table.AnyAsync(expression);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void SoftDelete(TEntity entity)
        {
            entity.IsDeleted = true;
            Table.Update(entity);
        }

        public void Update(TEntity entity)
        {
            Table.Update(entity);
        }
    }
}
