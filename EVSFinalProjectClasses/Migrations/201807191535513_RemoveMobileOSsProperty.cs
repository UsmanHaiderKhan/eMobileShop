namespace EVSFinalProjectClasses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveMobileOSsProperty : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Mobiles", "MobileOS_Id", "dbo.MobileOS");
            DropIndex("dbo.Mobiles", new[] { "MobileOS_Id" });
            DropColumn("dbo.Mobiles", "MobileOS_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Mobiles", "MobileOS_Id", c => c.Int());
            CreateIndex("dbo.Mobiles", "MobileOS_Id");
            AddForeignKey("dbo.Mobiles", "MobileOS_Id", "dbo.MobileOS", "Id");
        }
    }
}
