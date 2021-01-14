using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace Project.Shop.DataAccess.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class, new()
    {
        IQueryable<TEntity> GetAll();
        Task<TEntity> GetBySelectedIdAsync(int id);
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);  
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity> DeleteAsync(int id);
    }
}