namespace MasterDeatils_WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderDeatils",
                c => new
                    {
                        DeatilId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.DeatilId)
                .ForeignKey("dbo.OrderMasters", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.OrderMasters",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        Customername = c.String(),
                        ImagePath = c.String(),
                    })
                .PrimaryKey(t => t.OrderId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDeatils", "ProductId", "dbo.Products");
            DropForeignKey("dbo.OrderDeatils", "OrderId", "dbo.OrderMasters");
            DropIndex("dbo.OrderDeatils", new[] { "ProductId" });
            DropIndex("dbo.OrderDeatils", new[] { "OrderId" });
            DropTable("dbo.Products");
            DropTable("dbo.OrderMasters");
            DropTable("dbo.OrderDeatils");
        }
    }
}
