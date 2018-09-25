using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVSFinalProjectClasses.UserManagment
{
    public class LocationHandler
    {
        public List<Country> Getcountry()
        {
            dbcontext db = new dbcontext();
            using (db)
            {
                return (from u in db.Countries select u).ToList();
            }

        }
        public Country GetCountryById(int? id)
        {
            dbcontext db = new dbcontext();
            using (db)
            {
                return (from c in db.Countries where c.Id == id select c).FirstOrDefault();
            }
        }

        public void DeleteCountry(Country country)
        {
            dbcontext db = new dbcontext();
            using (db)
            {
                //db.Entry(user).State = EntityState.Modified;
                db.Entry(country).State = EntityState.Deleted;
                db.Countries.Remove(country);
                db.SaveChanges();
            }
        }
        public List<City> GetCitiesByCountry(Country country)
        {
            dbcontext db = new dbcontext();
            using (db)
            {
                return (from c in db.Cities
                        where c.Counrty.Id ==
                        country.Id
                        select c).ToList();
            }

        }
        public void AddCity(City city)
        {
            dbcontext db = new dbcontext();
            using (db)
            {
                db.Entry(city.Counrty).State = EntityState.Unchanged;
                db.Cities.Add(city);
                db.SaveChanges();
            }
        }
        public City GetCityById(int? id)
        {
            dbcontext db = new dbcontext();
            using (db)
            {
                return (from c in db.Cities
                        where c.Id == id
                        select c).FirstOrDefault();
            }
        }
        public void DelCity(City city)
        {
            dbcontext db = new dbcontext();
            using (db)
            {
                db.Entry(city).State = EntityState.Deleted;
                db.Cities.Remove(city);
                db.SaveChanges();
            }
        }
        public List<City> GetCities(Country country)
        {
            dbcontext db = new dbcontext();
            using (db)
            {
                return (from u in db.Cities
                        where u.Counrty.Id == country.Id
                        select u).ToList();
            }
        }
        public Country country(int id)
        {
            dbcontext db = new dbcontext();
            using (db)
            {
                return (from u in db.Countries where u.Id == id select u).FirstOrDefault();
            }

        }

    }
}
