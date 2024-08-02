namespace ShoppingCart.Models;

public class CartState
{
    public List<CartItem> Items { get; set; } = new List<CartItem>();
    public decimal Subtotal { get; set; }
    public decimal Tax { get; set; }
    public decimal Total { get; set; }
}