namespace EVSFinalProjectClasses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class MobileRelaseDate : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.Mobiles", "ReleaseDate", c => c.DateTime());
            //DropColumn("dbo.Mobiles", "DateTime");
        }

        public override void Down()
        {
            AddColumn("dbo.Mobiles", "DateTime", c => c.DateTime());
            DropColumn("dbo.Mobiles", "ReleaseDate");
        }
    }
}
