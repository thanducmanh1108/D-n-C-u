using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using baithi.Models;

namespace baithi.Controllers
{
    public class HomeController : Controller
    {
        private quanlydocauEntities db = new quanlydocauEntities();
        private quanlydocauEntities context = null;
        public List<sanpham> ListSp_sap_het()
        {
            var list = context.Database.SqlQuery<sanpham>("sp_sap_het").ToList();
            return list;
        }
        public ActionResult Index()
        {
            if (Session["tk"] != null)
            {
                var sp = db.sanphams.ToList();
                ViewBag.sp_saphet = db.sanphams.Where(m => m.soluong < 30).OrderBy(s => s.soluong).ToList();
                ViewBag.sp_tonkho = db.sanphams.Where(m => m.soluong > 100).OrderByDescending(s => s.soluong).ToList();
                ViewBag.tong_doanh_thu = db.OrderDetails.Sum(g => g.tonghoadon);

                return View(sp);
            }    
            return Redirect("~/Home/Login");

        }
        [HttpGet]

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string user_name, string pass_word)
        {
            var obj = db.Taikhoans.Where(m => m.user_name.Equals(user_name) && m.pass_word.Equals(pass_word)).FirstOrDefault();
            if (obj == null)
            {
                return View();
            }
            else
            {
                Session["tk"] = user_name;
                return Redirect("~/Home/Index");
            }    
        }
        public ActionResult logout()
        {
            Session.Clear();
            return Redirect("Login");
        }
        public ActionResult About()
        {
            var sp = db.sanphams.ToList();
            ViewBag.sp_moi = db.sanphams.OrderByDescending(s => s.IDsp).ToList();
            ViewBag.sp_can_cau_ca = db.sanphams.Where(m => m.loai == 5).OrderBy(s => s.IDsp).ToList();
            ViewBag.sp_may_cau_ca = db.sanphams.Where(m => m.loai == 3).OrderBy(s => s.IDsp).ToList();
            ViewBag.sp_moi_cau_ca = db.sanphams.Where(m => m.loai == 6).OrderBy(s => s.IDsp).ToList();

            return View(sp);

           
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult SPbanchay()
        {
            if (Session["user_name"] == null)
                return Redirect("~/Home/login");
            return View(db.sanphams.ToList());
        }
    }
}