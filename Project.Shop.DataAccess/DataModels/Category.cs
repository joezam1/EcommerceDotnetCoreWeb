using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Project.Shop.DataAccess.UnitOfWork;

namespace Project.Shop.DataAccess.DataModels
{
    [JsonObject(IsReference = true)]
    public class Category :UnitOfWorkEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsInactive { get; set; }

        [JsonIgnore] 
        [IgnoreDataMember] 
        public virtual ICollection<Product> Product {get; set;}
    }
}