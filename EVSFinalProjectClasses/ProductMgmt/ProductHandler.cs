using EVSFinalProjectClasses.UserManagment;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace EVSFinalProjectClasses.ProductMgmt
{
    public class ProductHandler
    {
        public List<Mobile> GetMobiles()
        {
            dbcontext db = new dbcontext();
            using (db)
            {
                return
                    (from m in
                        db.Mobiles
                            .Include(x => x.MobileBrand)
                            .Include(x => x.MobileOsVersion.MobileOS)
                            .Include(x => x.Images)
                     select m).ToList();
            }
        }

        public Mobile GetMobiles(int id)
        {
            dbcontext db = new dbcontext();
            using (db)
            {
                return (
                    from m in db.Mobiles
                        .Include(x => x.MobileBrand)
                        .Include(x => x.MobileOsVersion.MobileOS)
                        .Include(x => x.Images)
                    where m.Id == id
                    select m).FirstOrDefault();
            }
        }

        public List<Mobile> GetLatestMobiles(int counter)
        {
            dbcontext db = new dbcontext();
            using (db)
            {
                return (from m in db.Mobiles
                    .Include(x => x.MobileBrand)
                    .Include(x => x.MobileOsVersion.MobileOS)
                    .Include(x => x.Images)
                        orderby m.ReleaseDate descending
                        select m).Take(counter).ToList();

            }
        }

        public List<Mobile> GetMobilesByBrands(MobileBrand brand)
        {
            dbcontext db = new dbcontext();
            using (db)
            {
                return
                    (from m in
                        db.Mobiles
                            .Include(x => x.MobileBrand)
                            .Include(x => x.MobileOsVersion.MobileOS)
                            .Include(x => x.Images)
                     where m.MobileBrand.Id == brand.Id
                     select m).ToList();
            }
        }

        //public void AddCartitem(List<Cartitem> c)
        //{
        //    ProductContext context = new ProductContext();


        //    context.cartitem.AddRange(c);
        //    context.SaveChanges();
        //}

        public List<MobileBrand> GetBrands()
        {
            dbcontext db = new dbcontext();
            using (db)
            {
                return (from b in db.MobileBrands select b).ToList();
            }
        }

        public List<MobileOS> GetOperatingSystem()
        {
            dbcontext db = new dbcontext();
            using (db)
            {
                return (from oss in db.MobileOss select oss).ToList();

            }
        }
        public MobileOS GetOperatingSystemById(int? id)
        {
            dbcontext db = new dbcontext();
            using (db)
            {
                return (from c in db.MobileOss where c.Id == id select c).FirstOrDefault();
            }
        }
        public void DeleteOperatingSystem(MobileOS os)
        {
            dbcontext db = new dbcontext();
            using (db)
            {
                db.Entry(os).State = EntityState.Deleted;
                db.MobileOss.Remove(os);
                db.SaveChanges();
            }
        }
        public List<MobileOSVersion> GetMobileOsVersions(MobileOS os)
        {
            dbcontext db = new dbcontext();
            using (db)
            {
                return (from osv in db.MobileOsVersions where osv.MobileOS.Id == os.Id select osv).ToList();
            }
        }
        public List<MobileOSVersion> GetOSVersionByOS(MobileOS os)
        {
            dbcontext db = new dbcontext();
            using (db)
            {
                return (from c in db.MobileOsVersions
                        where c.MobileOS.Id ==
                              os.Id
                        select c).ToList();
            }

        }
        public void AddOSVersion(MobileOSVersion osv)
        {
            dbcontext db = new dbcontext();
            using (db)
            {
                db.Entry(osv.MobileOS).State = EntityState.Unchanged;
                db.MobileOsVersions.Add(osv);
                db.SaveChanges();
            }
        }
        public MobileOSVersion GetOSVersionById(int? id)
        {
            dbcontext db = new dbcontext();
            using (db)
            {
                return (from c in db.MobileOsVersions
                        where c.Id == id
                        select c).FirstOrDefault();
            }
        }
        public void DeleteOSVersion(MobileOSVersion mov)
        {
            dbcontext db = new dbcontext();
            using (db)
            {
                db.Entry(mov).State = EntityState.Deleted;
                db.MobileOsVersions.Remove(mov);
                db.SaveChanges();
            }
        }
        public MobileOS GetMobileOSById(int? id)
        {
            dbcontext db = new dbcontext();
            using (db)
            {
                return (from c in db.MobileOss where c.Id == id select c).FirstOrDefault();
            }
        }
        public void Add(Mobile mobile)
        {
            dbcontext db = new dbcontext();
            using (db)
            {
                db.Entry(mobile.MobileBrand).State = EntityState.Unchanged;
                db.Entry(mobile.MobileOsVersion).State = EntityState.Unchanged;
                db.Mobiles.Add(mobile);
                db.SaveChanges();
            }
        }
        public List<PlaceOrder> GetOrders()
        {
            dbcontext db = new dbcontext();
            using (db)
            {
                return (from c in db.PlaceOrders.Include(m => m.CartItems) select c).ToList();
            }

        }

        public PlaceOrder GetOrderById(int id)
        {
            dbcontext db = new dbcontext();
            using (db)
            {
                return (from c in db.PlaceOrders.Include(m => m.CartItems) where c.Id == id select c).FirstOrDefault();
            }
        }

        public MobileBrand GetBrandById(int id)
        {
            using (dbcontext db = new dbcontext())
            {
                return (from b in db.MobileBrands
                        where b.Id == id
                        select b).FirstOrDefault();
            }
        }
        public List<Slider> GetBrand()
        {
            dbcontext db = new dbcontext();
            using (db)
            {
                return (from c in db.Sliders select c).ToList();
            }
        }

        public Mobile GetMobileById(int id)
        {
            dbcontext db = new dbcontext();
            using (db)
            {
                return (from m in db.Mobiles
                    .Include(m => m.Images)
                    .Include(m => m.MobileBrand)
                        where m.Id == id
                        select m).FirstOrDefault();
            }
        }

        public void UpdateMobile(Mobile mob)
        {
            dbcontext db = new dbcontext();
            using (db)
            {
                db.Entry(mob).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public List<Contact> GetAllContacts()
        {
            dbcontext db = new dbcontext();
            using (db)
            {
                return (from c in db.Contacts select c).ToList();
            }
        }
        public int GetOrderIdByUser(int id)
        {
            dbcontext db = new dbcontext();
            using (db)
            {
                return (from c in db.PlaceOrders where c.User_Id == id select c).FirstOrDefault().Id;

            }
        }

        public PlaceOrder GetOrderByOrderId(int orderId)
        {
            dbcontext db = new dbcontext();
            using (db)
            {
                return (from c in db.PlaceOrders.Include(m => m.CartItems) where c.Id == orderId select c).FirstOrDefault();

            }
        }

        public PlaceOrder GetUserOrders(int User_id)
        {
            dbcontext db = new dbcontext();
            using (db)
            {
                return (from c in db.PlaceOrders.Include(m => m.CartItems) where c.User_Id == User_id select c).FirstOrDefault();
            }
        }
        public List<PlaceOrder> GetCartItemsByUser(int User_id)
        {
            dbcontext db = new dbcontext();
            using (db)
            {
                return (from c in db.PlaceOrders.Include(m => m.CartItems) where c.User_Id == User_id select c).ToList();

            }
        }
    }
}
