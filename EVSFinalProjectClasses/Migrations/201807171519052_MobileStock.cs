namespace EVSFinalProjectClasses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MobileStock : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Mobiles", "Stock", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Mobiles", "Stock");
        }
    }
}
