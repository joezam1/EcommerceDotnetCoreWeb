using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Project.Shop.DataAccess.DataModels
{
     [JsonObject(IsReference = true)] 
    public class ProductSize
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductSizeId { get; set; }
        public int ProductId { get; set; }
        [JsonIgnore] 
        [IgnoreDataMember] 
        public Product Product { get; set; }
        public int SizeId { get; set; }
        public Size Size { get; set; }
        public int StockLevel { get; set; }
        public int MinimumReorderLevel { get; set; }

    }
}