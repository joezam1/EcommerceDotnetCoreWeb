using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Project.Shop.DataAccess.EFContext;
using Project.Shop.DataAccess.Interfaces;

namespace Project.Shop.DataAccess.Repositories
{
    public class GenericRepository<TEntity>: IGenericRepository<TEntity> where TEntity : class, new()
    {
        protected readonly DataContext RepositoryPatternDemoContext;

        public GenericRepository(DataContext repositoryPatternDemoContext)
        {
            RepositoryPatternDemoContext = repositoryPatternDemoContext;
        }
        
        public IQueryable<TEntity> GetAll()
        {
            try
            {
                return RepositoryPatternDemoContext.Set<TEntity>();
            }
            catch(Exception ex)
            {
                throw new Exception($"couln't retrieve entities {ex.Message}. Errpr: {ex}");
            }
        }

        public async Task<TEntity> GetBySelectedIdAsync(int id)
        {
            var result = await RepositoryPatternDemoContext.Set<TEntity>().FindAsync(id);
            return result;
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
           var result = RepositoryPatternDemoContext.Set<TEntity>().Where(predicate).AsEnumerable<TEntity>();
           return result;
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }
            try
            {
                await RepositoryPatternDemoContext.AddAsync(entity);
                await RepositoryPatternDemoContext.SaveChangesAsync();
                return entity;
            
            }
            catch(Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be saved: {ex.Message}");
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException($"{nameof(UpdateAsync)} entity must not be null");
            }

            try
            {            
                RepositoryPatternDemoContext.Set<TEntity>().Attach(entity);   
                var result = RepositoryPatternDemoContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                
                await RepositoryPatternDemoContext.SaveChangesAsync();
                
                
                return entity;
            }
            catch(Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be updated: {ex.Message}");
            }
        }
        public async Task<TEntity> DeleteAsync(int id)
        {
            TEntity existing =await RepositoryPatternDemoContext.Set<TEntity>().FindAsync(id);
            if(existing !=null)
            {
                RepositoryPatternDemoContext.Set<TEntity>().Remove(existing);
                await RepositoryPatternDemoContext.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentNullException($"{nameof(existing)} entity is null");
            }
            return existing;
        }

    }
}