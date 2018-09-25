namespace EVSFinalProjectClasses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMobileId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CartItems", "MobileId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CartItems", "MobileId");
        }
    }
}
