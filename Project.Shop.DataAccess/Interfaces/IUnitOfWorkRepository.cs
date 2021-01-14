using System.Threading.Tasks;
using Project.Shop.DataAccess.EFContext;
using Project.Shop.DataAccess.UnitOfWork;

namespace Project.Shop.DataAccess.Interfaces
{
    public interface IUnitOfWorkRepository
    {
        DataContext _context {get; }
        Task<T> FindEntityAsync<T>(T entity)where T : class, new();
        Task<T> GetBySelectedIdAsync<T>(int id) where T : class, new();
        void SetEntitiesToInsert(UnitOfWorkEntity entity);
        void SetEntitiesToUpdate(UnitOfWorkEntity entity);
        void SetEntitiesToDelete(UnitOfWorkEntity entity);
        Task<int> Commit();
    }
}