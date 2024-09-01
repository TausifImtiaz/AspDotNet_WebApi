using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MasterDeatils_WebApi.Models
{
    public class OrderMaster
    {
        [Key]
        public int OrderId { get; set; }
        public string Customername { get; set; }
        public string ImagePath { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile {get; set;}
    

        public virtual List<OrderDeatil> OrderDeatil { get; set; } = new List<OrderDeatil>();
    }
}