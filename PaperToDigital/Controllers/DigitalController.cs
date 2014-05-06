using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PaperToDigital.Models;

namespace PaperToDigital.Controllers
{
    public class DigitalController : Controller
    {
        private DigitalContext db = new DigitalContext();

        // GET: /Digital/
        public ActionResult Index()
        {
            return View(db.Digitals.ToList());
        }

        // GET: /Digital/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Digital digital = db.Digitals.Find(id);
            if (digital == null)
            {
                return HttpNotFound();
            }
            return View(digital);
        }

        // GET: /Digital/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Digital/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,unique,title,desc,date,language,creator,publisher,subject,category,rights,ocr,image,md5,page")] Digital digital)
        {
            if (ModelState.IsValid)
            {
                db.Digitals.Add(digital);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(digital);
        }

        // GET: /Digital/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Digital digital = db.Digitals.Find(id);
            if (digital == null)
            {
                return HttpNotFound();
            }
            return View(digital);
        }

        // POST: /Digital/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,unique,title,desc,date,language,creator,publisher,subject,category,rights,ocr,image,md5,page")] Digital digital)
        {
            if (ModelState.IsValid)
            {
                db.Entry(digital).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(digital);
        }

        // GET: /Digital/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Digital digital = db.Digitals.Find(id);
            if (digital == null)
            {
                return HttpNotFound();
            }
            return View(digital);
        }

        // POST: /Digital/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Digital digital = db.Digitals.Find(id);
            db.Digitals.Remove(digital);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
