using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Angular.CSharp.Training.Data.Repository
{
    public class BaseRepository<T> where T : class
    {
        protected readonly DemoDbContext context;
        protected readonly DbSet<T> dbSet;

        public BaseRepository(DemoDbContext demoDbContext)
        {
            context = demoDbContext ?? throw new ArgumentNullException(nameof(demoDbContext));
            dbSet = context.Set<T>();
        }

        public async Task InsertAsync(T entity)
        {
            dbSet.Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            T entity = await dbSet.FindAsync(id);
            dbSet.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }
    }
}