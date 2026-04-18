using LearnNow.Class.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnNow.Class
{
    public class RepositoryGeneric<T> : IRepositoryGeneric<T> where T : class, IEntity
    {
        private readonly LearnNowDB db;
        private readonly DbSet<T> dbSet;

        public RepositoryGeneric(LearnNowDB db)
        {
            this.db = db;
            dbSet=db.Set<T>();
        }

        public int CountCourses()
        {
            return dbSet.Count();
        }

        public async virtual Task<int> Create(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            dbSet.Add(entity);
            await db.SaveChangesAsync();
            return entity.Id;
        }

        public async Task Delete(int id)
        {
            var e=await dbSet.FirstOrDefaultAsync(e=>e.Id==id);
            dbSet.Remove(e);
            await db.SaveChangesAsync();
        }

        public virtual async Task<ICollection<T>> GetAll()
        {
            return await dbSet.ToListAsync() ?? new List<T>().ToList();
        }

        public virtual async Task<T> GetById(int id)
        {
            var e =await dbSet.FirstOrDefaultAsync(e => e.Id==id);
            return e;
        }

        public virtual async Task Update(T entity)
        {
            if(db.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Update(entity);
            await db.SaveChangesAsync();
        }

    
    }
}
