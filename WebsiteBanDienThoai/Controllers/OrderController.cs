using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebsiteBanDienThoai.Models.Entities;

namespace WebsiteBanDienThoai.Controllers
{
    public class OrderController : Controller
    {
        private WebDienThoaiEntities db = new WebDienThoaiEntities();

        // GET: Orders
        // Hiển thị danh sách đơn hàng
        public ActionResult Index()
        {
            string accountID = User.Identity.GetUserId();
            
            // Lọc danh sách đơn hàng của người dùng hiện tại
            var orders = db.Orders.Where(o => o.AccountID == accountID);

            return View(orders.ToList());
        }


        // GET: Donhangs/Details/5
        //Hiển thị chi tiết đơn hàng
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order  order = db.Orders.Find(id);
            var chitiet = db.OrderDetails.Include(d => d.Product).Where(d => d.OrderID == id).ToList();
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(chitiet);
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
