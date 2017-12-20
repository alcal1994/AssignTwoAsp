using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Assignment2.Models;

namespace Assignment2.Controllers
{
    public class AthletesController : Controller
    {
        //old direct db connection - now in models/ EFAthletesRepository
        // private DataContext db = new DataContext();

        //repo link
        private IAthletesRepository db;

        //if param passed to constructor, use EF Repository & DbContext
        public AthletesController()
        {
            this.db = new EFAthletesRepository();
        }

        //if mock repo object passed to constructor, use Mock interface for unit testing
        public AthletesController(IAthletesRepository smRepo)
        {
            this.db = smRepo;
        }

        // GET: Athletes
        public ViewResult Index()
        {
            var athletes = db.Athletes.Include(a => a.Sport);
            return View(athletes.ToList().OrderBy(a=>a.FullName).ToList());
        }

        // GET: Athletes/Details/5
        public ViewResult Details(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            Athlete athlete = db.Athletes.SingleOrDefault(a => a.Pk_Athlete_Id == id);
            if (athlete == null)
            {
                return View("Error");
            }
            return View(athlete);
        }

        // GET: Athletes/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Fk_Sport_Id = new SelectList(db.Sports, "Pk_Sport_Id", "Sport1");
            return View("Create");
        }

        // POST: Athletes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Pk_Athlete_Id,FullName,Email,MobileNo,Fk_Sport_Id")] Athlete athlete)
        {
            if (ModelState.IsValid)
            {
             
                //scaffold code for inserting
                // db.Athletes.Add(athlete);
                //  db.SaveChanges();

                //new repository code for inserting
                db.Save(athlete);
                return RedirectToAction("Index");
            }

            ViewBag.Fk_Sport_Id = new SelectList(db.Sports, "Pk_Sport_Id", "Sport1", athlete.Fk_Sport_Id);
            return View("Create",athlete);
        }

        // GET: Athletes/Edit/5
        [Authorize]
        public ViewResult Edit(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            //scaffold code
            //Athlete athlete = db.Athletes.Find(id);

            //new repository code replacing line above
            Athlete athlete = db.Athletes.SingleOrDefault(a => a.Pk_Athlete_Id == id);

            if (athlete == null)
            {
                return View("Error");
            }
            ViewBag.Fk_Sport_Id = new SelectList(db.Sports, "Pk_Sport_Id", "Sport1", athlete.Fk_Sport_Id);
            return View( athlete);
        }

        // POST: Athletes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "Pk_Athlete_Id,FullName,Email,MobileNo,Fk_Sport_Id")] Athlete athlete)
        {
            if (ModelState.IsValid)
            {
                // scaffold code - old
                //db.Entry(athlete).State = EntityState.Modified;
                // db.SaveChanges();

                //repository code - new 
                db.Save(athlete);

                return RedirectToAction("Index");
            }
            ViewBag.Fk_Sport_Id = new SelectList(db.Sports, "Pk_Sport_Id", "Sport1", athlete.Fk_Sport_Id);
            return View("Edit", athlete);
        }

        // GET: Athletes/Delete/5
        [Authorize]
        public ViewResult Delete(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            //scaffold code
            //Athlete athlete = db.Athletes.Find(id);

            //new repository code replacing line above
            Athlete athlete = db.Athletes.SingleOrDefault(a => a.Pk_Athlete_Id == id);

            if (athlete == null)
            {
                return View("Error");
            }
            return View(athlete);
        }

        // POST: Athletes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            //scaffold code
            //Athlete athlete = db.Athletes.Find(id);
            //db.Athletes.Remove(athlete);
            //db.SaveChanges();

            //new repository code replacing line above
            if (id == null)
            {
                return View("Error");
            }
            Athlete athlete = db.Athletes.SingleOrDefault(a => a.Pk_Athlete_Id == id);
            if (athlete == null)
            {
                return View("Error");
            }
            db.Delete(athlete);
          
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
