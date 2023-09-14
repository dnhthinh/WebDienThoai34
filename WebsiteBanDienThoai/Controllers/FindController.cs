using System;
using System.Linq;
using System.Web.Mvc;
using WebsiteBanDienThoai.Models.Entities;

public class FindController : Controller
{
    // GET: /Find/Index
    public ActionResult Index(string searchString)
    {
        WebDienThoaiEntities db = new WebDienThoaiEntities();
         var products = db.Products.Where(p => p.ProductName.Contains(searchString)).ToList();
        return PartialView("~/Views/Find/Products.cshtml", products);
    }
}
