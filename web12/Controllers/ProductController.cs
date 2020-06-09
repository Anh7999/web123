using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web12.Models;

namespace web12.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        web1Entities1 db = new web1Entities1();
        public ActionResult Index(string meta)
        {
            var v = from t in db.categories
                    where t.meta == meta
                    select t;

            return View(v.FirstOrDefault());
        }
        public ActionResult Detail (long id)
        {
            var v = from t in db.products
                    where t.id == id
                    select t;
            return View(v.FirstOrDefault());
        }
        //public ActionResult cart(long id)
        //{
        //    var v = from t in db.products
        //            where t.id == id
        //            select t;
        //    return View(v.FirstOrDefault());
        //}
    }
}