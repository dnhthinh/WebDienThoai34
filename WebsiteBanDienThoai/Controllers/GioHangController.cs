using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanDienThoai.Models;
using WebsiteBanDienThoai.Models.Entities;

namespace Ictshop.Controllers
{
    public class GioHangController : Controller
    {
      WebDienThoaiEntities   db = new WebDienThoaiEntities();
        // GET: GioHang

        //Lấy giỏ hàng 
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang == null)
            {
                //Nếu giỏ hàng chưa tồn tại thì mình tiến hành khởi tao list giỏ hàng (sessionGioHang)
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }
        //Thêm giỏ hàng
        public ActionResult ThemGioHang(int iMasp, string strURL)
        {
            Product sp = db.Products.SingleOrDefault(n => n.ProductID == iMasp);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Lấy ra session giỏ hàng
            List<GioHang> lstGioHang = LayGioHang();
            //Kiểm tra sp này đã tồn tại trong session[giohang] chưa
            GioHang sanpham = lstGioHang.Find(n => n.ProductID == iMasp);
            if (sanpham == null)
            {
                sanpham = new GioHang(iMasp);
                //Add sản phẩm mới thêm vào list
                lstGioHang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.Quantity++;
                return Redirect(strURL);
            }
        }
        //Cập nhật giỏ hàng 
        public ActionResult CapNhatGioHang(int iMaSP, FormCollection f)
        {
            //Kiểm tra masp
            Product sp = db.Products.SingleOrDefault(n => n.ProductID == iMaSP);
            //Nếu get sai masp thì sẽ trả về trang lỗi 404
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Lấy giỏ hàng ra từ session
            List<GioHang> lstGioHang = LayGioHang();
            //Kiểm tra sp có tồn tại trong session["GioHang"]
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.ProductID == iMaSP);
            //Nếu mà tồn tại thì chúng ta cho sửa số lượng
            if (sanpham != null)
            {
                sanpham.Quantity = int.Parse(f["txtSoLuong"].ToString());

            }
            return RedirectToAction("GioHang");
        }
        //Xóa giỏ hàng
        public ActionResult XoaGioHang(int iMaSP)
        {
            //Kiểm tra masp
            Product sp = db.Products.SingleOrDefault(n => n.ProductID == iMaSP);
            //Nếu get sai masp thì sẽ trả về trang lỗi 404
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Lấy giỏ hàng ra từ session
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.ProductID == iMaSP);
            //Nếu mà tồn tại thì chúng ta cho sửa số lượng
            if (sanpham != null)
            {
                lstGioHang.RemoveAll(n => n.ProductID == iMaSP);

            }
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("GioHang");
        }
        //Xây dựng trang giỏ hàng

        public ActionResult GioHang()
        {           
            List<GioHang> lstGioHang = LayGioHang();
            return View(lstGioHang);
        }
        //Tính tổng số lượng và tổng tiền
        //Tính tổng số lượng
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                iTongSoLuong = lstGioHang.Sum(n => n.Quantity);
            }
            return iTongSoLuong;
        }
        //Tính tổng thành tiền
        private double TongTien()
        {
            double dTongTien = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                dTongTien = lstGioHang.Sum(n => n.ThanhTien);
            }
            return dTongTien;
        }
        //tạo partial giỏ hàng
        public ActionResult GioHangPartial()
        {
            if (TongSoLuong() == 0)
            {
                return PartialView();
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }
        //Xây dựng 1 view cho người dùng chỉnh sửa giỏ hàng
        public ActionResult SuaGioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> lstGioHang = LayGioHang();
            return View(lstGioHang);

        }


        //Xây dựng chức năng đặt hàng
        [HttpPost]
        public ActionResult DatHang(FormCollection donhangForm)
        {
            
            //Kiểm tra giỏ hàng
            if (Session["GioHang"] == null)
            {
                RedirectToAction("Index", "Home");
            }
            Console.WriteLine(donhangForm);
            

            //Thêm đơn hàng
            Order ddh = new Order();
            string accountID = User.Identity.GetUserId().ToString();
            ddh.AccountID = accountID;
            ddh.Status = 0;
            List<GioHang> gh = LayGioHang();
            ddh.OrderDate = DateTime.Now;
            decimal tongtien = 0;
            foreach (var item in gh)
            {
                decimal thanhtien = item.Quantity * (decimal)item.Price;
                tongtien += thanhtien;
            }
            ddh.Total = tongtien;
            db.Orders.Add(ddh);
            db.SaveChanges();
            //Thêm chi tiết đơn hàng
            foreach (var item in gh)
            {
                OrderDetail ctDH = new OrderDetail();
                decimal thanhtien = item.Quantity * (decimal)item.Price;
                ctDH.OrderID = ddh.OrderID;
                ctDH.ProductID = item.ProductID;
                ctDH.Quantity = item.Quantity;
                ctDH.Price = item.Price;
                ctDH.Total = (decimal)thanhtien;
                db.OrderDetails.Add(ctDH);
            }
            db.SaveChanges();
            Session["GioHang"] = null;
            return RedirectToAction("Index", "Order");
        }


        public ActionResult ThanhToanDonHang(bool isEmptyCart)
        {
            if (isEmptyCart)
            {
                TempData["EmptyCartMessage"] = "There are no items in your shopping cart.";
                return RedirectToAction("GioHang"); // Redirect back to the cart page
            }
            //Thêm đơn hàng
            Order order = new Order();
            string accountID = User.Identity.GetUserId();
            order.AccountID = accountID;
            List<GioHang> gh = LayGioHang();
            decimal tongtien = 0;
            foreach (var item in gh)
            {
                decimal thanhtien = item.Quantity * (decimal)item.Price;
                tongtien += thanhtien;
            }
            order.OrderDate = DateTime.Now;
            OrderDetail ctDH = new OrderDetail();
            ViewBag.tongtien = tongtien;
            db.SaveChanges();
            return View(order);

        }

    }
}