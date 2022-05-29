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
    public class hdchitietsController : Controller
    {
        private quanlydocauEntities db = new quanlydocauEntities();

        // GET: hdchitiets
        public ActionResult Index()
        {
            var cart = Session["CartSession"];
            var list = new List<CartItem>();
            if (cart != null)
            {   
                list = (List<CartItem>)cart;
                ViewBag.Card = list;
                foreach(var item in list)
                {
                    var spitem=db.sanphams.Find(item.sanpham.IDsp);
                    spitem.soluong -= item.Quantity;
                    db.SaveChanges();
                }
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        //// GET: hdchitiets/Details/5
        //public async Task<ActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    hdchitiet hdchitiet = await db.hdchitiets.FindAsync(id);
        //    if (hdchitiet == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(hdchitiet);
        //}

        //// GET: hdchitiets/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: hdchitiets/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create([Bind(Include = "IDhoadon,IDsp,IDkhachhang,soluong,dongia,thanhtien,tensp,IDnv,IDchietkhau")] hdchitiet hdchitiet)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.hdchitiets.Add(hdchitiet);
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    return View(hdchitiet);
        //}

        //// GET: hdchitiets/Edit/5
        //public async Task<ActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    hdchitiet hdchitiet = await db.hdchitiets.FindAsync(id);
        //    if (hdchitiet == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(hdchitiet);
        //}

        //// POST: hdchitiets/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "IDhoadon,IDsp,IDkhachhang,soluong,dongia,thanhtien,tensp,IDnv,IDchietkhau")] hdchitiet hdchitiet)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(hdchitiet).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    return View(hdchitiet);
        //}

        //// GET: hdchitiets/Delete/5
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    hdchitiet hdchitiet = await db.hdchitiets.FindAsync(id);
        //    if (hdchitiet == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(hdchitiet);
        //}

        //// POST: hdchitiets/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(int id)
        //{
        //    hdchitiet hdchitiet = await db.hdchitiets.FindAsync(id);
        //    db.hdchitiets.Remove(hdchitiet);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

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
