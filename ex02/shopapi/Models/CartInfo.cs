using System.Collections.Generic;
using System.Linq;

namespace shopapi.Models
{
    public class CartInfo
    {
        public IList<CartProduct> Products { get; set; }

        public double RawPrice => Products?.Sum(it => it.RawPrice) ?? 0;
        public double Discount => Products?.Sum(it => it.TotalDiscount) ?? 0;
        public double Price => RawPrice - Discount;
    }
}
