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
    public class nvsController : Controller
    {
        private quanlydocauEntities db = new quanlydocauEntities();

        // GET: nvs
        public async Task<ActionResult> Index()
        {
            return View(await db.nvs.ToListAsync());
        }

        // GET: nvs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            nv nv = await db.nvs.FindAsync(id);
            if (nv == null)
            {
                return HttpNotFound();
            }
            return View(nv);
        }

        // GET: nvs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: nvs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDnv,tennv,gioitinh,namsinh,diachi,sdt,chucvu,luong")] nv nv)
        {
            if (ModelState.IsValid)
            {
                db.nvs.Add(nv);
                await db.SaveChangesAsync();
                setAlert("Thêm nhân viên thành công!", "success");
                return RedirectToAction("Index");
            }

            return View(nv);
        }

        // GET: nvs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            nv nv = await db.nvs.FindAsync(id);
            if (nv == null)
            {
                return HttpNotFound();
            }
            return View(nv);
        }

        // POST: nvs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDnv,tennv,gioitinh,namsinh,diachi,sdt,chucvu,luong")] nv nv)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nv).State = EntityState.Modified;
                await db.SaveChangesAsync();
                setAlert("Sửa thông tin nhân viên thành công!", "success");
                return RedirectToAction("Index");
            }
            return View(nv);
        }

        // GET: nvs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            nv nv = await db.nvs.FindAsync(id);
            if (nv == null)
            {
                return HttpNotFound();
            }
            return View(nv);
        }

        // POST: nvs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            nv nv = await db.nvs.FindAsync(id);
            db.nvs.Remove(nv);
            await db.SaveChangesAsync();
            setAlert("Xóa nhân viên thành công", "success");
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
