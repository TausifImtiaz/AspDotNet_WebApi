using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MasterDeatils_WebApi.Models
{
    public class OrderDeatil
    {
        [Key] 
        public int DeatilId { get; set; }
        public int OrderId { get; set; }
        public OrderMaster OrderMaster { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal price { get; set; }
    }
}