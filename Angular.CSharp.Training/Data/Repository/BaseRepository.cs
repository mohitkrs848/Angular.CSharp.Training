using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
        public void Insert(T entity)
        {
            dbSet.Add(entity);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            T entity = dbSet.Find(id);
            dbSet.Remove(entity);
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public T GetById(int id)
        {
            return dbSet.Find(id);
        }
    }
}