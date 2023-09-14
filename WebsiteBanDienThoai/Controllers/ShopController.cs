using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanDienThoai.Models.Entities;
namespace WebsiteBanDienThoai.Controllers
{
    public class ShopController : Controller
    {
       
        // GET: Shop
        public ActionResult DanhSach()
        {
            //1. Lay danh sach du lieu trong bang
            


            return View();
        }

        // GET: Shop/Details/5
        public ActionResult Details(int id)
        {
            WebDienThoaiEntities db = new WebDienThoaiEntities();
            var ds = db.Products.Find(id);
            return View(ds);
        }

        // GET: Shop/Create
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult AboutUS()
        {
            return View();
        }

        // POST: Shop/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Shop/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Shop/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Shop/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Shop/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
