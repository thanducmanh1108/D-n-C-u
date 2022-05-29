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
    public class nccsController : Controller
    {
        private quanlydocauEntities db = new quanlydocauEntities();

        // GET: nccs
        public async Task<ActionResult> Index()
        {
            return View(await db.nccs.ToListAsync());
        }

        // GET: nccs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ncc ncc = await db.nccs.FindAsync(id);
            if (ncc == null)
            {
                return HttpNotFound();
            }
            return View(ncc);
        }

        // GET: nccs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: nccs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDncc,tenncc,sdt,diachi")] ncc ncc)
        {
            if (ModelState.IsValid)
            {
                db.nccs.Add(ncc);
                await db.SaveChangesAsync();
                setAlert("Thêm nhà cung cấp mới thành công", "success");
                return RedirectToAction("Index");
            }

            return View(ncc);
        }

        // GET: nccs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ncc ncc = await db.nccs.FindAsync(id);
            if (ncc == null)
            {
                return HttpNotFound();
            }
            return View(ncc);
        }

        // POST: nccs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDncc,tenncc,sdt,diachi")] ncc ncc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ncc).State = EntityState.Modified;
                await db.SaveChangesAsync();
                setAlert("Sửa nhà cung cấp thành công", "success");
                return RedirectToAction("Index");
            }
            return View(ncc);
        }

        // GET: nccs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ncc ncc = await db.nccs.FindAsync(id);
            if (ncc == null)
            {
                return HttpNotFound();
            }
            return View(ncc);
        }

        // POST: nccs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ncc ncc = await db.nccs.FindAsync(id);
            db.nccs.Remove(ncc);
            await db.SaveChangesAsync();
            setAlert("Xóa nhà cung cấp thành công", "success");
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
