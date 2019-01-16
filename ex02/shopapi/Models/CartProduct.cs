namespace shopapi.Models
{
    public class CartProduct
    {
        public ProductInfo Product { get; }
        public int Amount { get; }
        public double TotalDiscount { get; }
        public double RawPrice => Product.Price * Amount;
        public double TotalPrice => RawPrice - TotalDiscount;

        public CartProduct(ProductInfo product, int amount, double discount = 0)
        {
            Product = product;
            Amount = amount;
            TotalDiscount = discount;
        }
    }
}
