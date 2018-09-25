namespace EVSFinalProjectClasses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CartItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Single(nullable: false),
                        ImageUrl = c.String(),
                        Qauntity = c.Int(nullable: false),
                        Amount = c.Long(nullable: false),
                        PlaceOrder_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PlaceOrders", t => t.PlaceOrder_Id)
                .Index(t => t.PlaceOrder_Id);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Counrty_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.Counrty_Id)
                .Index(t => t.Counrty_Id);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Code = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Genders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MobileBrands",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ImageUrl = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MobileImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Caption = c.String(),
                        Perority = c.Int(nullable: false),
                        Url = c.String(),
                        Mobile_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Mobiles", t => t.Mobile_Id)
                .Index(t => t.Mobile_Id);
            
            CreateTable(
                "dbo.MobileOS",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MobileOSVersions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        MobileOS_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MobileOS", t => t.MobileOS_Id)
                .Index(t => t.MobileOS_Id);
            
            CreateTable(
                "dbo.Mobiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Single(nullable: false),
                        Description = c.String(),
                        BlueTooth = c.Boolean(nullable: false),
                        ThreeG = c.Boolean(nullable: false),
                        FourG = c.Boolean(nullable: false),
                        Wifi = c.Boolean(nullable: false),
                        Ram = c.Int(nullable: false),
                        FrontCam = c.Int(nullable: false),
                        BackCam = c.Int(nullable: false),
                        Color = c.String(),
                        ScreenSize = c.Int(nullable: false),
                        Battery = c.Int(nullable: false),
                        Accessories = c.String(),
                        Warranty = c.DateTime(nullable: false),
                        Features = c.String(),
                        DateTime = c.DateTime(),
                        MobileBrand_Id = c.Int(),
                        MobileOSs_Id = c.Int(),
                        MobileOsVersion_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MobileBrands", t => t.MobileBrand_Id)
                .ForeignKey("dbo.MobileOS", t => t.MobileOSs_Id)
                .ForeignKey("dbo.MobileOSVersions", t => t.MobileOsVersion_Id)
                .Index(t => t.MobileBrand_Id)
                .Index(t => t.MobileOSs_Id)
                .Index(t => t.MobileOsVersion_Id);
            
            CreateTable(
                "dbo.PlaceOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BuyerName = c.String(),
                        EmailAddress = c.String(),
                        FullAddress = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        Phone = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Rank = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sliders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BrandImage = c.String(),
                        Title = c.String(),
                        Description = c.String(),
                        Priority = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LoginId = c.String(),
                        Email = c.String(),
                        Password = c.String(maxLength: 50),
                        ConfirmPassword = c.String(),
                        FullName = c.String(),
                        Country = c.String(),
                        ImageUrl = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        BirthDate = c.DateTime(),
                        Phone = c.Double(nullable: false),
                        SecurityQuestion = c.String(),
                        SecurityAnswer = c.String(),
                        City_Id = c.Int(),
                        Gender_Id = c.Int(),
                        Role_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.City_Id)
                .ForeignKey("dbo.Genders", t => t.Gender_Id)
                .ForeignKey("dbo.Roles", t => t.Role_Id)
                .Index(t => t.City_Id)
                .Index(t => t.Gender_Id)
                .Index(t => t.Role_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.Users", "Gender_Id", "dbo.Genders");
            DropForeignKey("dbo.Users", "City_Id", "dbo.Cities");
            DropForeignKey("dbo.CartItems", "PlaceOrder_Id", "dbo.PlaceOrders");
            DropForeignKey("dbo.Mobiles", "MobileOsVersion_Id", "dbo.MobileOSVersions");
            DropForeignKey("dbo.Mobiles", "MobileOSs_Id", "dbo.MobileOS");
            DropForeignKey("dbo.Mobiles", "MobileBrand_Id", "dbo.MobileBrands");
            DropForeignKey("dbo.MobileImages", "Mobile_Id", "dbo.Mobiles");
            DropForeignKey("dbo.MobileOSVersions", "MobileOS_Id", "dbo.MobileOS");
            DropForeignKey("dbo.Cities", "Counrty_Id", "dbo.Countries");
            DropIndex("dbo.Users", new[] { "Role_Id" });
            DropIndex("dbo.Users", new[] { "Gender_Id" });
            DropIndex("dbo.Users", new[] { "City_Id" });
            DropIndex("dbo.Mobiles", new[] { "MobileOsVersion_Id" });
            DropIndex("dbo.Mobiles", new[] { "MobileOSs_Id" });
            DropIndex("dbo.Mobiles", new[] { "MobileBrand_Id" });
            DropIndex("dbo.MobileOSVersions", new[] { "MobileOS_Id" });
            DropIndex("dbo.MobileImages", new[] { "Mobile_Id" });
            DropIndex("dbo.Cities", new[] { "Counrty_Id" });
            DropIndex("dbo.CartItems", new[] { "PlaceOrder_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Sliders");
            DropTable("dbo.Roles");
            DropTable("dbo.PlaceOrders");
            DropTable("dbo.Mobiles");
            DropTable("dbo.MobileOSVersions");
            DropTable("dbo.MobileOS");
            DropTable("dbo.MobileImages");
            DropTable("dbo.MobileBrands");
            DropTable("dbo.Genders");
            DropTable("dbo.Countries");
            DropTable("dbo.Cities");
            DropTable("dbo.CartItems");
        }
    }
}
