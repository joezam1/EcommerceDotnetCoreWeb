using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Project.Shop.DataAccess.UnitOfWork;

namespace Project.Shop.DataAccess.DataModels
{
     [JsonObject(IsReference = true)] 
    public class Size :UnitOfWorkEntity
    {  
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SizeId { get; set; }   
        [Required]
        public string Description { get; set; }
        public bool IsInactive { get; set; }
        [JsonIgnore] 
        [IgnoreDataMember] 
        public ICollection<ProductSize> ProductSizes { get; set; }  
    }
}