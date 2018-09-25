namespace EVSFinalProjectClasses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMobileOS : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Mobiles", name: "MobileOSs_Id", newName: "MobileOS_Id");
            RenameIndex(table: "dbo.Mobiles", name: "IX_MobileOSs_Id", newName: "IX_MobileOS_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Mobiles", name: "IX_MobileOS_Id", newName: "IX_MobileOSs_Id");
            RenameColumn(table: "dbo.Mobiles", name: "MobileOS_Id", newName: "MobileOSs_Id");
        }
    }
}
