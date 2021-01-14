using System.Collections.Generic;
using System.Threading.Tasks;
using Project.Shop.DataAccess.DataModels;

namespace Project.Shop.BusinessLogic.Interfaces
{
    public interface ICategoryService
    {
        List<Category> GetAllCategories();
        Task<Category> GetSingleCategoryByIdAsync(int id);
        IEnumerable<Category> GetActiveCategories();
        Task<Category> CreateCategoryAsync(Category category);
        Task<int> UpdateCategoryAsync(Category category);
        Task<Category> DeleteCategoryAsync(int id);
        
    }
}