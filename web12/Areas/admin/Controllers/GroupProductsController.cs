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
    public class GroupProductsController : Controller
    {
        private web1Entities1 db = new web1Entities1();

        // GET: admin/GroupProducts
        public ActionResult Index()
        {
            return View(db.GroupProducts.ToList());
        }

        // GET: admin/GroupProducts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupProduct groupProduct = db.GroupProducts.Find(id);
            if (groupProduct == null)
            {
                return HttpNotFound();
            }
            return View(groupProduct);
        }

        // GET: admin/GroupProducts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/GroupProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,conntent,image,order,status")] GroupProduct groupProduct)
        {
            if (ModelState.IsValid)
            {
                db.GroupProducts.Add(groupProduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(groupProduct);
        }

        // GET: admin/GroupProducts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupProduct groupProduct = db.GroupProducts.Find(id);
            if (groupProduct == null)
            {
                return HttpNotFound();
            }
            return View(groupProduct);
        }

        // POST: admin/GroupProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,conntent,image,order,status")] GroupProduct groupProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(groupProduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(groupProduct);
        }

        // GET: admin/GroupProducts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupProduct groupProduct = db.GroupProducts.Find(id);
            if (groupProduct == null)
            {
                return HttpNotFound();
            }
            return View(groupProduct);
        }

        // POST: admin/GroupProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GroupProduct groupProduct = db.GroupProducts.Find(id);
            db.GroupProducts.Remove(groupProduct);
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
