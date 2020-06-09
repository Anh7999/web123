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
    public class conntactsController : Controller
    {
        private web1Entities1 db = new web1Entities1();

        // GET: admin/conntacts
        public ActionResult Index()
        {
            return View(db.conntacts.ToList());
        }

        // GET: admin/conntacts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            conntact conntact = db.conntacts.Find(id);
            if (conntact == null)
            {
                return HttpNotFound();
            }
            return View(conntact);
        }

        // GET: admin/conntacts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/conntacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,address,tel,mail")] conntact conntact)
        {
            if (ModelState.IsValid)
            {
                db.conntacts.Add(conntact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(conntact);
        }

        // GET: admin/conntacts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            conntact conntact = db.conntacts.Find(id);
            if (conntact == null)
            {
                return HttpNotFound();
            }
            return View(conntact);
        }

        // POST: admin/conntacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,address,tel,mail")] conntact conntact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(conntact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(conntact);
        }

        // GET: admin/conntacts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            conntact conntact = db.conntacts.Find(id);
            if (conntact == null)
            {
                return HttpNotFound();
            }
            return View(conntact);
        }

        // POST: admin/conntacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            conntact conntact = db.conntacts.Find(id);
            db.conntacts.Remove(conntact);
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
