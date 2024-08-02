using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Data;
using ShoppingCart.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShoppingCart.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CartController(ApplicationDbContext context) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;

        [HttpPost("add")]
        public async Task<IActionResult> AddProduct(int productId, int quantity)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                return NotFound();
            }

            var existingCartItem = await _context.CartItems.FirstOrDefaultAsync(item => item.Product.Id == productId);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity += quantity;
                _context.CartItems.Update(existingCartItem);
            }
            else
            {
                var cartItem = new CartItem { Product = product, Quantity = quantity };
                _context.CartItems.Add(cartItem);
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("remove")]
        public async Task<IActionResult> RemoveProduct(int productId)
        {
            var cartItem = await _context.CartItems.FirstOrDefaultAsync(item => item.Product.Id == productId);

            if (cartItem == null)
            {
                return NotFound();
            }

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
            return Ok();
        }
        
        [HttpGet]
        public async Task<ActionResult<CartState>> GetCartSate()
        {
            var cartItem = await _context.CartItems.Include(item => item.Product).ToListAsync();
            var subtotal = cartItem.Sum(item => item.Product.Price * item.Quantity);
            var tax = subtotal * 0.125m;
            var total = subtotal + tax;

            var cartState = new CartState
            {
                Items = cartItem,
                Subtotal = subtotal,
                Tax = tax,
                Total = total
            };
            return cartState;
            
        }
    }
    
   
}