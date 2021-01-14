using System.Collections.Generic;
using Newtonsoft.Json;
using Project.Shop.DataAccess.DataModels;

namespace Project.Shop.BusinessLogic.ViewModels
{
    public class ShopComponentsModel
    {
        [JsonProperty("products")]
        public List<Product> Products { get; set; }

        [JsonProperty("categories")]
        public List<Category> Categories { get; set; }

        [JsonProperty("statuses")]
        public List<Status> Statuses { get; set; }

        [JsonProperty("sizes")]
        public List<Size> Sizes { get; set; }
    }
}