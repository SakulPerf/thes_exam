namespace shopapi.Models
{
    public class CartProduct
    {
        public ProductInfo Product { get; }
        public int Amount { get; }
        public double TotalDiscount { get; }
        public double TotalPrice => (Product.Price * Amount) - TotalDiscount;

        public CartProduct(ProductInfo product, int amount, double discount = 0)
        {
            Product = product;
            Amount = amount;
            TotalDiscount = discount;
        }
    }
}
