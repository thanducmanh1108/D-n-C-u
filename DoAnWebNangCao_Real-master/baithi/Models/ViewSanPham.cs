using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace baithi.Models
{
    public class ViewSanPham
    {
        public int IDsp { get; set; }
        public string tensp { get; set; }
        public string size { get; set; }
        public int loai { get; set; }
        public string TenLoai { get; set; }
        public int hang { get; set; }
        public string TenHang { get; set; }
        public Nullable<int> soluong { get; set; }
        public string giathanh { get; set; }
        public string anh { get; set; }
        public int tenkho { get; set; }
        public string TenKho { get; set; }
        public string mota { get; set; }
    }
}