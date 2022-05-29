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
    public class kh_hangController : Controller
    {
        private quanlydocauEntities db = new quanlydocauEntities();

        // GET: kh_hang
        public async Task<ActionResult> Index()
        {
            return View(await db.kh_hang.ToListAsync());
        }

        // GET: kh_hang/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            kh_hang kh_hang = await db.kh_hang.FindAsync(id);
            if (kh_hang == null)
            {
                return HttpNotFound();
            }
            return View(kh_hang);
        }

        // GET: kh_hang/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: kh_hang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDkhachhang,tenkhachhang,sdt,diachi")] kh_hang kh_hang)
        {
            if (ModelState.IsValid)
            {
                db.kh_hang.Add(kh_hang);
                await db.SaveChangesAsync();
                setAlert("Thêm khách hàng thành công", "success");
                return RedirectToAction("Index");
            }

            return View(kh_hang);
        }

        // GET: kh_hang/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            kh_hang kh_hang = await db.kh_hang.FindAsync(id);
            if (kh_hang == null)
            {
                return HttpNotFound();
            }
            return View(kh_hang);
        }

        // POST: kh_hang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDkhachhang,tenkhachhang,sdt,diachi")] kh_hang kh_hang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kh_hang).State = EntityState.Modified;
                await db.SaveChangesAsync();
                setAlert("Sửa thông tin khách hàng thành công", "success");
                return RedirectToAction("Index");
            }
            return View(kh_hang);
        }

        // GET: kh_hang/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            kh_hang kh_hang = await db.kh_hang.FindAsync(id);
            if (kh_hang == null)
            {
                return HttpNotFound();
            }
            return View(kh_hang);
        }

        // POST: kh_hang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            kh_hang kh_hang = await db.kh_hang.FindAsync(id);
            db.kh_hang.Remove(kh_hang);
            await db.SaveChangesAsync();
            setAlert("Xóa khách hàng thành công", "success");
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
        protected void setAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            if (type == "success")
            {
                TempData["AlertType"] = "alert-success";
            }
            else if (type == "warning")
            {
                TempData["AlertType"] = "alert-warning";
            }
            else if (type == "error")
            {
                TempData["AlertType"] = "alert-danger";
            }
        }
    }
}
