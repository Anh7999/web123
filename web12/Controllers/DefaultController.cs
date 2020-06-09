using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web12.Models;

namespace web12.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        web1Entities1 db = new web1Entities1();
        public ActionResult Index()
        {
            return View();
        }
      
        public ActionResult getNews()
        {
            var v = from t in db.News
                    where t.hide == true
                    orderby t.order ascending
                    select t;
            return PartialView(v.ToList());
        }
        public ActionResult getCategory()
        {
            ViewBag.meta = "san-pham";
            var v = from t in db.categories
                    where t.hide == true
                    orderby t.order ascending
                    select t;
            return PartialView(v.ToList());
        }
        public ActionResult getProduct(long id, string metatitle)
        {
            ViewBag.meta = "san-pham";
         //   ViewBag.meta = metatitle;
            var v = from t in db.products
                    where  t.hdie == true && t.categoryid== id 
                    orderby t.order ascending
                    select t;

            return PartialView(v.ToList());
        }
        public ActionResult getProducts(long id, string metatitle)
        {
            ViewBag.meta = metatitle;
            var v = from t in db.products
                    where t.hdie == true
                    orderby t.order ascending
                    select t;

            return PartialView(v.ToList());
        }
    }
}
