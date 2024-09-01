using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MasterDeatils_WebApi.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext () : base ("dbcon")
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderMaster> OrderMasters { get; set; }
        public DbSet<OrderDeatil> OrderDeatils { get; set;}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDeatil>()
                .HasRequired(o => o.OrderMaster)
                .WithMany(o => o.OrderDeatil)
                .HasForeignKey(o => o.OrderId);
        }

    }
}