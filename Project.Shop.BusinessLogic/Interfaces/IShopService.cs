using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.Shop.BusinessLogic.Interfaces
{
    public interface IShopService
    {
         Task<string> GetAllStoreComponentsAsync();
    }
}