using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//added references 
using System.Web;
using System.Data.Entity;

namespace Assignment2.Models
{
    public class EFAthletesRepository : IAthletesRepository
    {
        //db connection
        private DataContext db = new DataContext();

        public IQueryable<Athlete> Athletes { get { return db.Athletes; } } 

        public IQueryable<Sport> Sports { get { return db.Sports; } }

        public IQueryable<Athlete> Athlete => throw new NotImplementedException();

        public void Delete(Athlete athlete)
        {
            db.Athletes.Remove(athlete);
            db.SaveChanges();
        }

        public Athlete Save(Athlete athlete)
        {
            if(athlete.Pk_Athlete_Id == 0)
            {
                db.Athletes.Add(athlete);
            }
            else
            {
                db.Entry(athlete).State = EntityState.Modified;
            }
            db.SaveChanges();

            return athlete;
        }
    }
}
