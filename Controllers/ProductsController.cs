using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Data;
using ShoppingCart.Models;

namespace ShoppingCart.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerator<Product>>> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            _context.Products.Add(product);
             await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProducts), new { id = product.Id }, product);
        }
    }

}