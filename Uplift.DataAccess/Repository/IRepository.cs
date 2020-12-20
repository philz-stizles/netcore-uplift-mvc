using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Uplift.DataAccess.Repository
{
    public interface IRepository<T> where T: class
    {
        Task Add(T entity);
        Task<T> Get(int id);
        Task<T> Get(Expression<Func<T, bool>> filter = null, 
            string includes = null);
        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, 
            string includes = null);
        Task Remove(int id);
        void Remove(T entity);
    }
}