using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToolsStoreAPI.Data;
using ToolsStoreAPI.Models;

namespace ToolsStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize, ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DataContext _context;

        public ProductsController(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all Products
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            return Ok(await _context.Products.ToListAsync());
        }

        /// <summary>
        /// Create a new product
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     {        
        ///       "productName": "Makita screwdriver",
        ///       "category": "Electrical",
        ///       "price": 60,
        ///       "unitsInStock": 5
        ///     }
        /// </remarks>
        [HttpPost]
        public async Task<ActionResult> PostProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        /// Edit an exist product
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            var dbProduct = await _context.Products.FindAsync(id);
            if(dbProduct == null)
                return BadRequest("Product not found.");

            dbProduct.ProductName = product.ProductName;
            dbProduct.Price = product.Price;
            dbProduct.Category = product.Category;
            dbProduct.UnitsInStock = product.UnitsInStock;

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
