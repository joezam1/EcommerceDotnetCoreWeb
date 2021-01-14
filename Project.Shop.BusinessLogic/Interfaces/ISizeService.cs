using System.Collections.Generic;
using System.Threading.Tasks;
using Project.Shop.DataAccess.DataModels;

namespace Project.Shop.BusinessLogic.Interfaces
{
    public interface ISizeService
    {
        List<Size> GetAllSizes();
        Task<Size> GetSingleSizeByIdAsync(int id);
        IEnumerable<Size> GetActiveSizes();
        Task<Size> CreateSizeAsync(Size size);
        Task<int> UpdateSizeAsync(Size size);
        Task<Size> DeleteSizeAsync(int id);
    }
}