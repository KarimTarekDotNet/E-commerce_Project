using Ecom.Core.Interfaces;
using Ecom.Infrastucture.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ecom.Infrastucture.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _Context;

        public GenericRepository(AppDbContext context)
        {
            _Context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _Context.Set<T>().AddAsync(entity);
            await _Context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _Context.Set<T>().FindAsync(id);
            _Context.Set<T>().Remove(entity!);
            await _Context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync() => await _Context.Set<T>().AsNoTracking().ToListAsync();

        public async Task<IReadOnlyList<T>> GetAllAsync(params Expression<Func<T, object>>[] include)
        {
            var query = _Context.Set<T>().AsNoTracking().AsQueryable();

            foreach (var i in include)
            {
                query = query.Include(i);
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await _Context.Set<T>().FindAsync(id);
            return entity!;
        }

        public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] include)
        {
            var query = _Context.Set<T>().AsQueryable();
            foreach (var i in include)
            {
                query = query.Include(i);
            }
            var entity = await query.FirstOrDefaultAsync(x => EF.Property<int>(x, "Id") == id);
            return entity!;
        }

        public async Task UpdateAsync(T entity)
        {
            _Context.Entry(entity).State = EntityState.Modified;
            await _Context.SaveChangesAsync();
        }
    }
}