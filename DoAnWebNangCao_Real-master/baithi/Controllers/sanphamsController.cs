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
using System.IO;

namespace baithi.Controllers
{
    public class sanphamsController : Controller
    {
        private quanlydocauEntities db = new quanlydocauEntities();

        // GET: sanphams
        public ActionResult Index(string searchString)
        {
            List<sanpham> links = db.sanphams.ToList();

            if (!String.IsNullOrEmpty(searchString)) // kiểm tra chuỗi tìm kiếm có rỗng/null hay không
            {
                links = links.Where(s => s.tensp.Contains(searchString)).ToList(); //lọc theo chuỗi tìm kiếm
            }
            List<loai> loaiLst = db.loais.ToList();
            List<hang> hangLst = db.hangs.ToList();
            List<khohang> khoLst = db.khohangs.ToList();
            var list = from a in links
                       join b in loaiLst on a.loai equals b.IDloai
                       join c in hangLst on a.hang equals c.IDhang
                       join d in khoLst on a.tenkho equals d.IDkho
                       select new ViewSanPham
                       {
                           IDsp = a.IDsp,
                           tensp = a.tensp,
                           size = a.size,
                           loai = a.loai.Value,
                           TenLoai = b.tenloai,
                           hang = a.hang.Value,
                           TenHang = c.tenhang,
                           soluong = a.soluong,
                           giathanh = a.giathanh,
                           tenkho = a.tenkho.Value,
                           TenKho = d.tenkho,
                           anh = a.anh,
                           mota = a.mota
                       };

            return View(list.ToList()); //trả về kết quả
        }

        // GET: sanphams/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sanpham sanpham = await db.sanphams.FindAsync(id);
            if (sanpham == null)
            {
                return HttpNotFound();
            }
            return View(sanpham);
        }

        // GET: sanphams/Create
        [ValidateInput(false)]
        public ActionResult Create()
        {
            List<loai> cate_loai = db.loais.ToList();
            SelectList cateList = new SelectList(cate_loai, "IDloai", "tenloai");
            ViewBag.LoaiList = cateList;

            List<hang> cate_hang = db.hangs.ToList();
            SelectList cateList_hang = new SelectList(cate_hang, "IDhang", "tenhang");
            ViewBag.HangList = cateList_hang;

            List<khohang> cate_kho = db.khohangs.ToList();
            SelectList cateList_kho = new SelectList(cate_kho, "IDkho", "tenkho");
            ViewBag.KhoList = cateList_kho;

            return View();
        }

        // POST: sanphams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Create([Bind(Include = "IDsp,tensp,size,loai,hang,soluong,giathanh,anh,tenkho,mota")] sanpham sanpham, HttpPostedFileBase anh)
        {
            
            if (ModelState.IsValid)
            {
                if (anh != null)
                {
                    string filename = Path.GetFileName(anh.FileName);
                    string url_img = Server.MapPath("~/Images/" + filename);
                    anh.SaveAs(url_img);

                    sanpham.anh = filename;
                }
                db.sanphams.Add(sanpham);
                await db.SaveChangesAsync();
                setAlert("Thêm sản phẩm thành công", "success");
                return RedirectToAction("Index");
            }

            return View(sanpham);
        }

        // GET: sanphams/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sanpham sanpham = await db.sanphams.FindAsync(id);
            if (sanpham == null)
            {
                return HttpNotFound();
            }

            List<loai> cate_loai = db.loais.ToList();
            SelectList cateList = new SelectList(cate_loai, "IDloai", "tenloai");
            ViewBag.LoaiList = cateList;

            List<hang> cate_hang = db.hangs.ToList();
            SelectList cateList_hang = new SelectList(cate_hang, "IDhang", "tenhang");
            ViewBag.HangList = cateList_hang;

            List<khohang> cate_kho = db.khohangs.ToList();
            SelectList cateList_kho = new SelectList(cate_kho, "IDkho", "tenkho");
            ViewBag.KhoList = cateList_kho;

            return View(sanpham);
        }

        // POST: sanphams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDsp,tensp,size,loai,hang,soluong,giathanh,anh,tenkho,mota")] sanpham sanpham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanpham).State = EntityState.Modified;
                await db.SaveChangesAsync();
                setAlert("Sửa sản phẩm thành công", "success");
                return RedirectToAction("Index");
            }
            return View(sanpham);
        }

        // GET: sanphams/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sanpham sanpham = await db.sanphams.FindAsync(id);
            if (sanpham == null)
            {
                return HttpNotFound();
            }
            return View(sanpham);
        }

        // POST: sanphams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            sanpham sanpham = await db.sanphams.FindAsync(id);
            db.sanphams.Remove(sanpham);
            await db.SaveChangesAsync();
            setAlert("Xóa sản phẩm thành công", "success");
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
