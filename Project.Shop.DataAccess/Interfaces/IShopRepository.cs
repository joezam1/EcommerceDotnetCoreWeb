using System.Collections.Generic;
using System.Threading.Tasks;
using Project.Shop.DataAccess.UnitOfWork;

namespace Project.Shop.DataAccess.Interfaces
{
    public interface IShopRepository
    {
        Task<List<object>> GetComponentsAsync();
    }
}