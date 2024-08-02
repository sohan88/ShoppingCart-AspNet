namespace ShoppingCart.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public required Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
