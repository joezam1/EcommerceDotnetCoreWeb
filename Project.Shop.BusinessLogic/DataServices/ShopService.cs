using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Shop.BusinessLogic.Interfaces;
using Project.Shop.BusinessLogic.ViewModels;
using Project.Shop.DataAccess.DataModels;
using Project.Shop.DataAccess.EFContext;
using Project.Shop.DataAccess.Interfaces;

namespace Project.Shop.BusinessLogic.DataServices
{
    public class ShopService : IShopService
    {
        private readonly IJsonHelper _jsonHelper;
        private readonly IShopRepository _shopRepository;
        public ShopService(IShopRepository shopRepository, IJsonHelper jsonHelper)
        {
            _shopRepository = shopRepository;
            _jsonHelper = jsonHelper;
            
        }
        public async Task<string> GetAllStoreComponentsAsync()
        {
            var allComponents =await _shopRepository.GetComponentsAsync();
   
            var components = new ShopComponentsModel(){
                Products = (List<Product>)allComponents[0],
                Categories = (List<Category>)allComponents[1],
                Statuses = (List<Status>)allComponents[2],
                Sizes  = (List<Size>)allComponents[3]
            };
            
            string objectsJson = _jsonHelper.JsonSerializeObject(components);
            
            return objectsJson;
        }
    }
}