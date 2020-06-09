using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using web12.Models;

namespace web12.Areas.admin.Controllers
{
    public class customesController : Controller
    {
        private web1Entities1 db = new web1Entities1();

        // GET: admin/customes
        public ActionResult Index()
        {
            return View(db.customes.ToList());
        }

        // GET: admin/customes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            custome custome = db.customes.Find(id);
            if (custome == null)
            {
                return HttpNotFound();
            }
            return View(custome);
        }

        // GET: admin/customes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/customes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,date,province,phone,email")] custome custome)
        {
            if (ModelState.IsValid)
            {
                db.customes.Add(custome);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(custome);
        }

        // GET: admin/customes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            custome custome = db.customes.Find(id);
            if (custome == null)
            {
                return HttpNotFound();
            }
            return View(custome);
        }

        // POST: admin/customes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,date,province,phone,email")] custome custome)
        {
            if (ModelState.IsValid)
            {
                db.Entry(custome).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(custome);
        }

        // GET: admin/customes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            custome custome = db.customes.Find(id);
            if (custome == null)
            {
                return HttpNotFound();
            }
            return View(custome);
        }

        // POST: admin/customes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            custome custome = db.customes.Find(id);
            db.customes.Remove(custome);
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
