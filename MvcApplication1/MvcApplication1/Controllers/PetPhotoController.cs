using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.Models;

namespace MvcApplication1.Controllers
{
    public class PetPhotoController : Controller
    {
        private Entities1 db = new Entities1();

        //
        // GET: /PetPhoto/

        public ActionResult Index()
        {
            var petphotoes = db.PetPhotoes.Include(p => p.Pet);
            return View(petphotoes.ToList());
        }

        //
        // GET: /PetPhoto/Details/5

        public ActionResult Details(int id = 0)
        {
            PetPhoto petphoto = db.PetPhotoes.Find(id);
            if (petphoto == null)
            {
                return HttpNotFound();
            }
            return View(petphoto);
        }

        //
        // GET: /PetPhoto/Create

        public ActionResult Create()
        {
            ViewBag.PetID = new SelectList(db.Pets, "PetID", "PetName");
            return View();
        }

        //
        // POST: /PetPhoto/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PetPhoto petphoto)
        {
            if (ModelState.IsValid)
            {
                db.PetPhotoes.Add(petphoto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PetID = new SelectList(db.Pets, "PetID", "PetName", petphoto.PetID);
            return View(petphoto);
        }

        //
        // GET: /PetPhoto/Edit/5

        public ActionResult Edit(int id = 0)
        {
            PetPhoto petphoto = db.PetPhotoes.Find(id);
            if (petphoto == null)
            {
                return HttpNotFound();
            }
            ViewBag.PetID = new SelectList(db.Pets, "PetID", "PetName", petphoto.PetID);
            return View(petphoto);
        }

        //
        // POST: /PetPhoto/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PetPhoto petphoto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(petphoto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PetID = new SelectList(db.Pets, "PetID", "PetName", petphoto.PetID);
            return View(petphoto);
        }

        //
        // GET: /PetPhoto/Delete/5

        public ActionResult Delete(int id = 0)
        {
            PetPhoto petphoto = db.PetPhotoes.Find(id);
            if (petphoto == null)
            {
                return HttpNotFound();
            }
            return View(petphoto);
        }

        //
        // POST: /PetPhoto/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PetPhoto petphoto = db.PetPhotoes.Find(id);
            db.PetPhotoes.Remove(petphoto);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}