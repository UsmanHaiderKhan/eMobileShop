namespace EVSFinalProjectClasses.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EVSFinalProjectClasses.UserManagment.dbcontext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "EVSFinalProjectClasses.UserManagment.dbcontext";
        }

        protected override void Seed(EVSFinalProjectClasses.UserManagment.dbcontext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
