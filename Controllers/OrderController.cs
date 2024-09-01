using MasterDeatils_WebApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace MasterDetails_WebApi.Controllers
{
    public class OrderController : ApiController
    {
        private MyDbContext _db = new MyDbContext();

        public IHttpActionResult GetOrder()
        {
            var data = _db.OrderMasters.ToList();
            var jsonResult = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };
            var serial = JsonConvert.SerializeObject(data, Formatting.None, jsonResult);
            var jsonObject = JsonConvert.DeserializeObject(serial);
            return Ok(jsonObject);
        }

        public IHttpActionResult GetOrder(int id)
        {
            OrderMaster data = _db.OrderMasters.FirstOrDefault(o => o.OrderId == id);
            var jsonResult = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };
            var serial = JsonConvert.SerializeObject(data, Formatting.None, jsonResult);
            var jsonObject = JsonConvert.DeserializeObject(serial);
            return Ok(jsonObject);
        }

        public IHttpActionResult DeleteOrder(int id)
        {
            var order = _db.OrderMasters.FirstOrDefault(o => o.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }
            _db.OrderMasters.Remove(order);
            _db.SaveChanges();
            return Ok();
        }

        public IHttpActionResult PostOrder()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            // Customer Section
            var order = new OrderMaster()
            {
                Customername = HttpContext.Current.Request.Form["Customername"]
            };

            // Image Section
            var imageFile = HttpContext.Current.Request.Files["ImageFile"];
            if (imageFile != null)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetFileName(imageFile.FileName);
                var imagePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Images"), fileName);
                order.ImagePath = imagePath;
                imageFile.SaveAs(imagePath);
            }

            // Order Detail Section
            var detail = HttpContext.Current.Request.Form["OrderDetail"];
            if (detail != null)
            {
                var orderList = JsonConvert.DeserializeObject<List<OrderDeatil>>(detail);
                order.OrderDeatil = orderList;
            }
            _db.OrderMasters.Add(order);
            _db.SaveChanges();
            return Ok();
        }


        public IHttpActionResult PutOrder(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var order = _db.OrderMasters.Include(o => o.OrderDeatil).FirstOrDefault(o => o.OrderId == id);

            // Customer Section
            order.Customername = HttpContext.Current.Request.Form["CustomerName"];

            // Image Section
            var ImageFile = HttpContext.Current.Request.Files["ImageFile"];
            if (ImageFile != null)
            {
                var filename = Guid.NewGuid().ToString() + Path.GetFileName(ImageFile.FileName);
                var imagepath = Path.Combine(HttpContext.Current.Server.MapPath("~/Images"), filename);
                order.ImagePath = imagepath;
                ImageFile.SaveAs(imagepath);
            }

            // Order Detail Section
            var detail = HttpContext.Current.Request.Form["OrderDetail"];
            if (!string.IsNullOrEmpty(detail))
            {
                var ordList = JsonConvert.DeserializeObject<List<OrderDeatil>>(detail);
                order.OrderDeatil.Clear();
                order.OrderDeatil.AddRange(ordList);
            }
            _db.Entry(order).State = EntityState.Modified;
            _db.SaveChanges();
            return Ok();
        }



    }
}
