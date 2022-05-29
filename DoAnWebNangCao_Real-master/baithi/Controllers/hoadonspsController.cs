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
    public class hoadonspsController : Controller
    {
        private quanlydocauEntities db = new quanlydocauEntities();

        // GET: hoadonsps
        public async Task<ActionResult> Index()
        {
            return View(await db.hoadonsps.ToListAsync());
        }

        // GET: hoadonsps/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hoadonsp hoadonsp = await db.hoadonsps.FindAsync(id);
            if (hoadonsp == null)
            {
                return HttpNotFound();
            }
            return View(hoadonsp);
        }

        // GET: hoadonsps/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: hoadonsps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDhoadon,IDkhachhang,ngayban,tongtien")] hoadonsp hoadonsp)
        {
            if (ModelState.IsValid)
            {
                db.hoadonsps.Add(hoadonsp);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(hoadonsp);
        }

        // GET: hoadonsps/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hoadonsp hoadonsp = await db.hoadonsps.FindAsync(id);
            if (hoadonsp == null)
            {
                return HttpNotFound();
            }
            return View(hoadonsp);
        }

        // POST: hoadonsps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDhoadon,IDkhachhang,ngayban,tongtien")] hoadonsp hoadonsp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hoadonsp).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(hoadonsp);
        }

        // GET: hoadonsps/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hoadonsp hoadonsp = await db.hoadonsps.FindAsync(id);
            if (hoadonsp == null)
            {
                return HttpNotFound();
            }
            return View(hoadonsp);
        }

        // POST: hoadonsps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            hoadonsp hoadonsp = await db.hoadonsps.FindAsync(id);
            db.hoadonsps.Remove(hoadonsp);
            await db.SaveChangesAsync();
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
