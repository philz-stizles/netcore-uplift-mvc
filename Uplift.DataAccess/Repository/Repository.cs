using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Uplift.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext Context;
        internal DbSet<T> dbSet;

        public Repository(DbContext context)
        {
            Context = context;
            this.dbSet = Context.Set<T>();
        }

        public async Task Add(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public async Task<T> Get(Expression<Func<T, bool>> filter = null, string includes = null)
        {
            var query = dbSet.AsQueryable();

            if(filter != null){
                query = query.Where(filter);
            }

            if(includes != null || !string.IsNullOrEmpty(includes)){
                foreach(var include in includes.Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(include);
                }
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<T> Get(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includes = null)
        {
            var query = dbSet.AsQueryable();

            if(filter != null){
                query = query.Where(filter);
            }

            if(includes != null || !string.IsNullOrEmpty(includes)){
                foreach(var include in includes.Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(include);
                }
            }

            if(orderBy != null){
                return await orderBy(query).ToListAsync();
            }

            return await query.ToListAsync();
        }

        public async Task Remove(int id)
        {
            var entity = await dbSet.FindAsync(id);
            Remove(entity);
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }
    }
}