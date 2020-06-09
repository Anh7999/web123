using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using web12.Help;
using web12.Models;

namespace web12.Areas.admin.Controllers
{
    public class productsController : Controller
    {
        private web1Entities1 db = new web1Entities1();
       // private object selectedId;

        // GET: admin/products
        public ActionResult Index(long? id=null)
        {
            getCategory(id);
            //  var products = db.products.Include(p => p.category).Include(p => p.GroupProduct);
            //  return View(products.ToList());
            return View();
        }
        public void getCategory(long? selectedId=null)
        {
            ViewBag.Category = new SelectList(db.categories.Where(x => x.hide == true)
                .OrderBy(x => x.order), "id", "name", selectedId);
        }
        // GET: admin/products/Details/5
        public ActionResult getProduct(long? id)
        {
            if(id==null)
            {
                var v = db.products.OrderBy(x => x.order).ToList();
                return PartialView(v);

            }
            var m = db.products.Where(x => x.categoryid == id).OrderBy(x => x.order).ToList();
            return PartialView(m);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: admin/products/Create
        public ActionResult Create()
        {
            //    ViewBag.categoryid = new SelectList(db.categories, "id", "name");
            //    ViewBag.groupproduct_id = new SelectList(db.GroupProducts, "id", "name");
            getCategory();
             return View();
        }

        // POST: admin/products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "id,name,price,img,img1,img2,img3,description,meta,size,color,hdie,order,datebegin,categoryid,produc,groupproduct_id")] product product, HttpPostedFile img)
        {
            try
            {
                var path = "";
                var filename = "";
                if (ModelState.IsValid)
                {
                    if (img != null)
                    {
                        filename = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss") + img.FileName;
                        path = Path.Combine(Server.MapPath("~/Conntent/uploat/img/product"), filename);
                        img.SaveAs(path);
                        product.img = filename;

                    }
                    else
                    {
                        product.img = "logo.png";
                    }
                    product.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                    product.meta = Functions.ConvertToUnSign(product.meta);
                    product.order = getMaxOrder(product.categoryid);
                    db.products.Add(product);
                    db.SaveChanges();
                    return RedirectToAction("Index", "products", new { id = product.categoryid });
                }
            }
            catch(DbEntityValidationException e)
            {
                throw e;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            //ViewBag.categoryid = new SelectList(db.categories, "id", "name", product.categoryid);
            //ViewBag.groupproduct_id = new SelectList(db.GroupProducts, "id", "name", product.groupproduct_id);
            return View(product);
        }
        public int getMaxOrder(long? CategoryId)
        {
            if (CategoryId == null)
                return 1;
            return db.products.Where(x => x.categoryid == CategoryId).Count();
        }
        // GET: admin/products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            //ViewBag.categoryid = new SelectList(db.categories, "id", "name", product.categoryid);
            //ViewBag.groupproduct_id = new SelectList(db.GroupProducts, "id", "name", product.groupproduct_id);
            return View(product);
        }

        // POST: admin/products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "id,name,price,img,img1,img2,img3,description,meta,size,color,hdie,order,datebegin,categoryid,produc,groupproduct_id")] product product, HttpPostedFile img)
        {
            var path = "";
            var filename = "";
            product temp = getById(product.id);
            if (ModelState.IsValid)
            {
                if (img != null)
                {
                    filename = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + img.FileName;
                    path = Path.Combine(Server.MapPath("~/Content/Upload/img/news"), filename);
                    img.SaveAs(path);
                    temp.img = filename;
                }
                temp.name = product.name;
                temp.price = product.price;
                temp.meta = Functions.ConvertToUnSign(product.meta);
                temp.order = product.order;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.categoryid = new SelectList(db.categories, "id", "name", product.categoryid);
            ViewBag.groupproduct_id = new SelectList(db.GroupProducts, "id", "name", product.groupproduct_id);
            return View(product);
        }
        public product getById(long id)
        {
            return db.products.Where(x => x.id == id).FirstOrDefault();
        }
        // GET: admin/products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: admin/products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            product product = db.products.Find(id);
            db.products.Remove(product);
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
