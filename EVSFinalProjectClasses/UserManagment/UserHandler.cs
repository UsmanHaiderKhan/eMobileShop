using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace EVSFinalProjectClasses.UserManagment
{
    public class UserHandler
    {
        public List<User> GetUser()
        {
            dbcontext db = new dbcontext();
            using (db)
            {
                return (from u in db.Users
                        .Include(x => x.Role)
                        .Include(x => x.Gender)
                        select u).ToList();
            }
        }
        public User GetUser(int id)
        {
            dbcontext db = new dbcontext();
            using (db)
            {
                return (from u in db.Users
                        .Include(x => x.Role)
                        .Include(x => x.Gender)
                        where u.Id == id
                        select u).FirstOrDefault();
            }
        }
        public void UpdateUsers(User u)
        {
            dbcontext db = new dbcontext();
            using (db)
            {
                db.Entry(u).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public User GetUserByEmail(string Email)
        {
            dbcontext db = new dbcontext();
            using (db)
            {
                return (from c in db.Users where c.Email == Email select c).FirstOrDefault();
            }
        }
        public User GetUser(string Loginid, string password)
        {
            dbcontext db = new dbcontext();
            using (db)
            {
                return (from u in db.Users.Include("Role")
                        where u.LoginId.Equals(Loginid) && u.Password.Equals(password)
                        select u).FirstOrDefault();

            }
        }
        public User AddUser(User u)
        {
            try
            {
                dbcontext db = new dbcontext();
                using (db)
                {
                    db.Entry(u.Role).State = System.Data.Entity.EntityState.Unchanged;
                    db.Entry(u.City).State = System.Data.Entity.EntityState.Unchanged;
                    db.Entry(u.Gender).State = EntityState.Unchanged;
                    db.Users.Add(u);
                    // db.Users.
                    db.SaveChanges();
                }
                return u;
            }
            catch
            {
                return null;
            }
        }
        public Role GetRoleById(int id)
        {
            dbcontext db = new dbcontext();
            using (db)
            {
                return (from c in db.Roles where c.Id == id select c).First();
            }
        }
        public List<Gender> GetGender()
        {
            dbcontext db = new dbcontext();
            using (db)
            {
                return (from c in db.Genders select c).ToList();
            }

        }
        public Gender GetGender(int id)
        {
            dbcontext db = new dbcontext();
            using (db)
            {
                return (from c in db.Genders where c.Id == id select c).Single();
            }

        }
        public Gender GetGenderByName(string name)
        {
            dbcontext db = new dbcontext();
            using (db)
            {
                return (from c in db.Genders where c.Name == name select c).Single();
            }

        }
        public User GetUserById(int id)
        {
            dbcontext db = new dbcontext();
            using (db)
            {
                return (from c in db.Users
                    .Include(x => x.Role)
                    .Include(x => x.Gender)
                        where c.Id == id
                        select c).FirstOrDefault();
            }
        }


    }
}
