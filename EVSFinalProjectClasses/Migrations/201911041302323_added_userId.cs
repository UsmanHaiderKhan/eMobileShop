namespace EVSFinalProjectClasses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_userId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlaceOrders", "User_Id", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PlaceOrders", "User_Id");
        }
    }
}
