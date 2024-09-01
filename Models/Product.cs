using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MasterDeatils_WebApi.Models
{
    public class Product
    {
        [Key] 
        public int ProductId { get; set; }
        public string Name { get; set; }    
        public virtual ICollection<OrderDeatil> Deatils {  get; set; }    
    }
}