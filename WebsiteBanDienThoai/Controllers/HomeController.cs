using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebsiteBanDienThoai.Models.Entities;

namespace WebsiteBanDienThoai.Controllers
{
    public class HomeController : Controller
    {
        WebDienThoaiEntities db = new WebDienThoaiEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Products()
        {
            List<Product> ds = db.Products.Where(p => p.Status == "0").ToList();
            return View(ds);
        }
       

    }

}
