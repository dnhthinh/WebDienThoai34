using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteBanDienThoai.Models.Entities;

namespace WebsiteBanDienThoai.Models
{
    public class GioHang
    {
        //private int iMaSP;

        //public int IMaSP
        //{
        //    get { return iMaSP; }
        //    set { iMaSP = value; }
        //}
        WebDienThoaiEntities db = new WebDienThoaiEntities();
        public int AccountID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public double ThanhTien
        {
            get { return Quantity * Price; }
        }
        //Hàm tạo cho giỏ hàng
        public GioHang(int Masp)
        {
            ProductID = Masp;
            Product sp = db.Products.Single(n => n.ProductID == ProductID);
            ProductName= sp.ProductName;
            Price = int.Parse(sp.Price.ToString());
            Quantity = 1;
        }

    }
}