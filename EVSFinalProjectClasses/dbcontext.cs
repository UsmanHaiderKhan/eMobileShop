using EVSFinalProjectClasses.ProductMgmt;
using System.Data.Entity;

namespace EVSFinalProjectClasses.UserManagment
{
    public class dbcontext : DbContext
    {
        public dbcontext() : base("constr")
        {
            Configuration.LazyLoadingEnabled = false;
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Mobile> Mobiles { get; set; }
        public DbSet<MobileOS> MobileOss { get; set; }
        public DbSet<MobileBrand> MobileBrands { get; set; }
        public DbSet<MobileOSVersion> MobileOsVersions { get; set; }
        public DbSet<MobileImage> MobileImages { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<PlaceOrder> PlaceOrders { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Contact> Contacts { get; set; }






    }
}
