using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Shop.DataAccess.DataModels;
using Project.Shop.DataAccess.EFContext;
using Project.Shop.DataAccess.Interfaces;
using Project.Shop.DataAccess.UnitOfWork;

namespace Project.Shop.DataAccess.Repositories
{
    public class ShopRepository : IShopRepository
    {
         private readonly DataContext _dataContext;

         public ShopRepository( DataContext dataContext)
         {
              _dataContext = dataContext;
         }
        public async Task<List<object>> GetComponentsAsync()
        {
            List<Product> products = await _dataContext.Products.Include(a =>a.ProductSizes).ToListAsync();
            List<Category> categories = await  _dataContext.Categories.ToListAsync();
            List<Status> statuses = await _dataContext.Statuses.ToListAsync();
            List<Size> sizes = await _dataContext.Sizes.ToListAsync();
            
            List<object> objects = new List<object>(){products,categories,statuses,sizes};

            return objects;
        }

    }
}