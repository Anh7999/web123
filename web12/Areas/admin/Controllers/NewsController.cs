using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using web12.Models;

namespace web12.Areas.admin.Controllers
{
    public class NewsController : Controller
    {
        private web1Entities1 db = new web1Entities1();

        // GET: admin/News
        public ActionResult Index()
        {
            return View(db.News.ToList());
        }

        // GET: admin/News/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // GET: admin/News/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "id,name,img,description,detail,meta,hide,order,datebegin")] News news,HttpPostedFileBase img)
        {
            var path = "";
            var filename = "";

            if (ModelState.IsValid)
            {
                if(img != null)
                {
                    filename = img.FileName;
                    path = Path.Combine(Server.MapPath("~/Content/upload/img/news"), filename);
                    img.SaveAs(path);
                    news.img = filename;
                }
                else
                {
                    news.img = "logo.png";
                }
                news.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                
                db.News.Add(news);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(news);
        }

        // GET: admin/News/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: admin/News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "id,name,img,description,detail,meta,hide,order,datebegin")] News news,HttpPostedFileBase img)
        {
            var path = "";
            var filename = "";
            News temp = getById(news.id);
            if (ModelState.IsValid)
            {
                if(img!=null)
                {
                    filename = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + img.FileName;
                    path = Path.Combine(Server.MapPath("~/Content/Upload/img/news"), filename);
                    img.SaveAs(path);
                    temp.img = filename;
                }
                temp.name = news.name;
                temp.description = news.description;
                temp.meta = news.meta;
                temp.hide = news.hide;
                temp.order = news.order;

                db.Entry(news).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(news);
        }public News getById(long id)
        {
            return db.News.Where(x => x.id == id).FirstOrDefault();
        }

        // GET: admin/News/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: admin/News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            News news = db.News.Find(id);
            db.News.Remove(news);
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
