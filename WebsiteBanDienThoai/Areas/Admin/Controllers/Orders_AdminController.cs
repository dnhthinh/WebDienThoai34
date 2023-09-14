using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebsiteBanDienThoai.Models.Entities;

namespace WebsiteBanDienThoai.Areas.Admin.Controllers
{
    public class Orders_AdminController : Controller
    {
        // GET: Admin/Orders_Admin
        WebDienThoaiEntities db = new WebDienThoaiEntities();
        public ActionResult Index()
        {
            WebDienThoaiEntities db = new WebDienThoaiEntities();
            List<Order> orders = db.Orders.ToList();
            return View(orders);
        }
        // GET: Admin/Orders_Admin/Edit/5
        // GET: Admin/Orders_Admin/Edit/5
        public ActionResult Edit(int id)
        {
            var order = db.Orders.Find(id);
            return View(order);
        }

        // POST: Admin/Orders_Admin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Order updatedOrder)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    // Tìm đơn hàng trong cơ sở dữ liệu
                    var order = db.Orders.Find(id);


                    // Cập nhật trạng thái của đơn hàng
                    order.Status = updatedOrder.Status;

                    // Lưu thay đổi vào cơ sở dữ liệu
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch
                {
                    // Xử lý lỗi nếu có
                    return View();
                }
            }
            return View(updatedOrder);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Admin/Products_Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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
