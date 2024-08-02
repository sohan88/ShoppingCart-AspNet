using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Models
{
    public class Product
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public decimal Price { get; set; }
    }
}