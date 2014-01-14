﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.Models;

namespace MvcApplication1.Controllers
{
    public class PetController : Controller
    {
        private Entities1 db = new Entities1();

        //
        // GET: /Pet/

        public ActionResult Index()
        {
            var pets = db.Pets.Include(p => p.Status).Include(p => p.UserProfile);
            return View(pets.ToList());
        }

        //
        // GET: /Pet/Details/5

        public ActionResult Details(int id = 0)
        {
            Pet pet = db.Pets.Find(id);
            if (pet == null)
            {
                return HttpNotFound();
            }
            return View(pet);
        }

        //
        // GET: /Pet/Create

        public ActionResult Create()
        {
            ViewBag.StatusID = new SelectList(db.Status, "StatusID", "Description");
            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName");
            return View();
        }

        //
        // POST: /Pet/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pet pet)
        {
            if (ModelState.IsValid)
            {
                db.Pets.Add(pet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StatusID = new SelectList(db.Status, "StatusID", "Description", pet.StatusID);
            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName", pet.UserId);
            return View(pet);
        }

        //
        // GET: /Pet/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Pet pet = db.Pets.Find(id);
            if (pet == null)
            {
                return HttpNotFound();
            }
            ViewBag.StatusID = new SelectList(db.Status, "StatusID", "Description", pet.StatusID);
            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName", pet.UserId);
            return View(pet);
        }

        //
        // POST: /Pet/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Pet pet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StatusID = new SelectList(db.Status, "StatusID", "Description", pet.StatusID);
            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName", pet.UserId);
            return View(pet);
        }

        //
        // GET: /Pet/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Pet pet = db.Pets.Find(id);
            if (pet == null)
            {
                return HttpNotFound();
            }
            return View(pet);
        }

        //
        // POST: /Pet/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pet pet = db.Pets.Find(id);
            db.Pets.Remove(pet);
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