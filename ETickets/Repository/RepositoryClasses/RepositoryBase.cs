using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ETickets.Data;
using Microsoft.EntityFrameworkCore;

namespace ETickets.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T: class
    {
        protected AppDBContext AppDBContext { get; set; }
        public RepositoryBase(AppDBContext appDBContext)
        {
            AppDBContext = appDBContext;
        }
        public IQueryable<T> FindAll() => AppDBContext.Set<T>().AsNoTracking();
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) => AppDBContext.Set<T>().Where(expression).AsNoTracking();

        public async Task Create(T entity) => await AppDBContext.Set<T>().AddAsync(entity);
        public void Update(T entity) => AppDBContext.Set<T>().Update(entity);
        public void Delete(T entity) => AppDBContext.Set<T>().Remove(entity);

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = AppDBContext.Set<T>();
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return await query.ToListAsync();
        }
    }
}
