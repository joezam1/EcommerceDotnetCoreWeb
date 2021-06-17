using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Project.Shop.BusinessLogic.Interfaces;
using Project.Shop.DataAccess.DataModels;
using Project.Shop.DataAccess.Interfaces;

namespace Project.Shop.BusinessLogic.DataServices
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _context ;

        public CategoryService(IGenericRepository<Category> context)
        {
            _context = context;
        }

        //Tested--
         public List<Category> GetAllCategories()
        {
            var result = _context.GetAll().ToList();
            return result;
        }
       
        //Tested--
        public IEnumerable<Category> GetActiveCategories()
        {
             var categories =_context.Get(a =>a.IsInactive == false).ToList();
            return categories;
        }

        //Tested--
        public async Task<Category> GetSingleCategoryByIdAsync(int id)
        {
            var selectedCategory =await _context.GetBySelectedIdAsync(id);
            return selectedCategory;
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            var result = await _context.AddAsync(category);
            return result;
        }
        
        //Tested--
        public async Task<int> UpdateCategoryAsync(Category category)
        {
            Category selectedCategory =await _context.GetBySelectedIdAsync(category.CategoryId);
            if(selectedCategory !=null)
            {
                selectedCategory.Name = category.Name;
                selectedCategory.Description = category.Description;
                selectedCategory.IsInactive = category.IsInactive;
                await _context.UpdateAsync(selectedCategory);
                return 1;
            }
            return 0;
        }

        //Tested--
        public async Task<Category> DeleteCategoryAsync(int id)
        {
            var result = await _context.DeleteAsync(id);
            return result;
        }
    }
}