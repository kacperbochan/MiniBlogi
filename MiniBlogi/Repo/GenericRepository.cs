using Microsoft.EntityFrameworkCore;
using MiniBlogi.Data;

namespace MiniBlogi.Repo
{
    public class GenericRepository<T> where T : class
    {
        protected readonly BlogDbContext _context;
        protected DbSet<T> DbSet;

        public GenericRepository(BlogDbContext context)
        {
            _context = context;
            DbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _context.AddAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await DbSet.FindAsync(id);
            DbSet.Remove(entity);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }

}
