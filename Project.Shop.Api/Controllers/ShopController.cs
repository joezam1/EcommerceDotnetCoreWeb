using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project.Shop.BusinessLogic.Interfaces;

namespace Project.Shop.Api.Controllers
{
    [Route("api/shop")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly IShopService _shopService;
        public ShopController(IShopService shopService)
        {
            _shopService = shopService;
        }
        // GET api/shop/all-components
        [HttpGet("all-components")]
         public async Task<ActionResult<string>> GetAllItemsForDisplayAsync()
        {
           var result = await _shopService.GetAllStoreComponentsAsync();
            return Ok(result);
        }
    }
}