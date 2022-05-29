using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using baithi.Models;

namespace baithi.Controllers
{
    public class banhangsController : Controller
    {
        private quanlydocauEntities db = new quanlydocauEntities();
        private const string CartSession = "CartSession";
        static int mahoadon;
        public ActionResult Index()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
                ViewBag.Card = list;
            }
            return View(db.sanphams.ToList());
        }
        public JsonResult Add(int quantity, int id)
        {
            var product = db.sanphams.Find(id);
            var cart = Session[CartSession];
            var qty = 0;
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.sanpham.IDsp == id))
                {
                    foreach (var item in list)
                    {
                        if (item.sanpham.IDsp == id)
                        {
                            item.Quantity += quantity;
                            qty = item.Quantity;
                        }
                    }
                }
                else
                {
                    var item = new CartItem();
                    item.sanpham = product;
                    item.Quantity = quantity;
                    qty = item.Quantity;
                    list.Add(item);
                }
                Session[CartSession] = list;
            }
            else
            {
                //tạo mới cart item
                var item = new CartItem();
                item.sanpham = product;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);
                //gán vào session
                Session[CartSession] = list;

            }
            return Json(new {status=true, qty}, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(int quantity, int id)
        {
            var product = db.sanphams.Find(id);
            var cart = Session[CartSession];
            var qty = 0;
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.sanpham.IDsp == id))
                {
                    foreach (var item in list)
                    {
                        if (item.sanpham.IDsp == id)
                        {
                            item.Quantity -= quantity;
                            qty = item.Quantity;
                        }
                    }
                }
                else
                {
                    var item = new CartItem();
                    item.sanpham = product;
                    item.Quantity = quantity;
                    qty = item.Quantity;
                    list.Add(item);
                }
                Session[CartSession] = list;
            }
            else
            {
                //tạo mới cart item
                var item = new CartItem();
                item.sanpham = product;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);
                //gán vào session
                Session[CartSession] = list;

            }
            return Json(new { status = true, qty }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult cong(int quantity, int id)
        {
            var product = db.sanphams.Find(id);
            var cart = Session[CartSession];
            var qty = 0;
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.sanpham.IDsp == id))
                {
                    foreach (var item in list)
                    {
                        if (item.sanpham.IDsp == id)
                        {
                            item.Quantity += quantity;
                            qty = item.Quantity;
                        }
                    }
                }
                else
                {
                    var item = new CartItem();
                    item.sanpham = product;
                    item.Quantity = quantity;
                    qty = item.Quantity;
                    list.Add(item);
                }
                Session[CartSession] = list;
            }
            else
            {
                //tạo mới cart item
                var item = new CartItem();
                item.sanpham = product;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);
                //gán vào session
                Session[CartSession] = list;

            }
            return Json(new { status = true, qty }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult tru(int quantity, int id)
        {
            var product = db.sanphams.Find(id);
            var cart = Session[CartSession];
            var qty = 0;
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.sanpham.IDsp == id))
                {
                    foreach (var item in list)
                    {
                        if (item.sanpham.IDsp == id)
                        {
                            item.Quantity -= quantity;
                            qty = item.Quantity;
                        }
                    }
                }
                else
                {
                    var item = new CartItem();
                    item.sanpham = product;
                    item.Quantity = quantity;
                    qty = item.Quantity;
                    list.Add(item);
                }
                Session[CartSession] = list;
            }
            else
            {
                //tạo mới cart item
                var item = new CartItem();
                item.sanpham = product;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);
                //gán vào session
                Session[CartSession] = list;

            }
            return Json(new { status = true, qty }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SendContact(kh_hang d)
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            var result = db.kh_hang.SingleOrDefault(x => x.tenkhachhang == d.tenkhachhang && x.sdt == d.sdt);
            if (result == null)
            {
                var kh = new kh_hang();
                kh.tenkhachhang = d.tenkhachhang;
                kh.sdt = d.sdt;
                kh.diachi = d.diachi;
                db.kh_hang.Add(kh);
                db.SaveChanges();
                var order = new Order();
                order.IDkhach = kh.IDkhachhang;
                db.Orders.Add(order);
                db.SaveChanges();
                mahoadon = order.ID;
                foreach (var item in list)
                {
                    var orderdetail = new OrderDetail();
                    orderdetail.idorder = order.ID;
                    orderdetail.idsanpham = item.sanpham.IDsp;
                    orderdetail.tensp = item.sanpham.tensp;
                    orderdetail.soluongmua = item.Quantity;
                    decimal Gia_thanh = Convert.ToDecimal(item.sanpham.giathanh);
                    orderdetail.tongtien = Gia_thanh * item.Quantity;
                    orderdetail.ngaymua = DateTime.Now;
                    orderdetail.tonghoadon = orderdetail.tongtien;
                    //decimal gia = decimal.Parse(item.sanpham.giathanh);
                    //orderdetail.tongtien = gia * item.Quantity;
                    db.OrderDetails.Add(orderdetail);
                    db.SaveChanges();
                }
            }
            else
            {
                var order = new Order();
                order.IDkhach = result.IDkhachhang;
                db.Orders.Add(order);
                db.SaveChanges();
                mahoadon = order.ID;
                foreach(var item in list)
                {
                    var orderdetail = new OrderDetail();
                    orderdetail.idorder = order.ID;
                    orderdetail.idsanpham = item.sanpham.IDsp;
                    orderdetail.soluongmua = item.Quantity;
                    orderdetail.tensp = item.sanpham.tensp;
                    decimal Gia_thanh = Convert.ToDecimal(item.sanpham.giathanh);
                    orderdetail.tongtien = Gia_thanh * item.Quantity;
                    orderdetail.ngaymua = DateTime.Now;
                    //decimal gia = decimal.Parse(item.sanpham.giathanh);
                    //orderdetail.tongtien = gia * item.Quantity;
                    
                    orderdetail.tonghoadon = orderdetail.tongtien;
                    db.OrderDetails.Add(orderdetail);
                    db.SaveChanges();
                }
            }
            list = (List<CartItem>)cart;
            ViewBag.Card = list;
            foreach (var item in list)
            {
                var spitem = db.sanphams.Find(item.sanpham.IDsp);
                spitem.soluong -= item.Quantity;
                db.SaveChanges();
            }
            
            Session[CartSession] = null;
            return Json(new { status=true});
        }
        public ActionResult Thanhtoan()
        {
            ViewBag.makhachhang = db.Orders.Find(mahoadon).IDkhach;
            ViewBag.mahoadon = mahoadon;
            return View(db.OrderDetails.Where(x=>x.idorder==mahoadon).ToList());
        }

    }
}
