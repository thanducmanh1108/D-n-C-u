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
    public class khohangsController : Controller
    {
        private quanlydocauEntities db = new quanlydocauEntities();

        // GET: khohangs
        public async Task<ActionResult> Index()
        {
            return View(await db.khohangs.ToListAsync());
        }

        // GET: khohangs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            khohang khohang = await db.khohangs.FindAsync(id);
            if (khohang == null)
            {
                return HttpNotFound();
            }
            return View(khohang);
        }

        // GET: khohangs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: khohangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDkho,tenkho,diachi")] khohang khohang)
        {
            if (ModelState.IsValid)
            {
                db.khohangs.Add(khohang);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(khohang);
        }

        // GET: khohangs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            khohang khohang = await db.khohangs.FindAsync(id);
            if (khohang == null)
            {
                return HttpNotFound();
            }
            return View(khohang);
        }

        // POST: khohangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDkho,tenkho,diachi")] khohang khohang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(khohang).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(khohang);
        }

        // GET: khohangs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            khohang khohang = await db.khohangs.FindAsync(id);
            if (khohang == null)
            {
                return HttpNotFound();
            }
            return View(khohang);
        }

        // POST: khohangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            khohang khohang = await db.khohangs.FindAsync(id);
            db.khohangs.Remove(khohang);
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
