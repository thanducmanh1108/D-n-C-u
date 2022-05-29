using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace baithi.Models
{
    [Serializable]
    public class CartItem
    {
        public sanpham sanpham { get; set; }
        public int Quantity { get; set; }
    }
}