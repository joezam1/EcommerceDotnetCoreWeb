using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Project.Shop.DataAccess.UnitOfWork;

namespace Project.Shop.DataAccess.DataModels
{
    [JsonObject(IsReference = true)] 
    public class Product :UnitOfWorkEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        public string SKU { get; set; }
        public string SerialNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsInactive { get; set; }
        public string ImageHref1 { get; set; }
        public string ImageHref2 { get; set; }
        public string ImageHref3 { get; set; }
        public string ImageHref4 { get; set; }
        public string ImageHref5 { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        [ForeignKey(nameof(Status))]
        public int StatusId { get; set; }
        
        [JsonIgnore] 
        [IgnoreDataMember] 
        public virtual Category Category {get; set;}
        
        [JsonIgnore] 
        [IgnoreDataMember] 
        public virtual Status Status {get; set;}

        [JsonIgnore] 
        [IgnoreDataMember] 
        public ICollection<ProductSize> ProductSizes { get; set; }     
    }
}