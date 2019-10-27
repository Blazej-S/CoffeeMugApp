using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoffeeMugTest.Models;

namespace CoffeeMugTest.Controllers
{
    [Produces("application/json")]
    [Route("api/ProductItems")]
    public class ProductController : Controller
    {
        private readonly ProductContext _context;

        public ProductController(ProductContext context)
        {
            _context = context;
        }

        // GET: api/ProductItems
        [HttpGet]
        public IEnumerable<ProductItem> GetProductItems()
        {
            return _context.ProductItems;
        }

        // GET: api/ProductItems/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductItem([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productItem = await _context.ProductItems.SingleOrDefaultAsync(m => m.Id == id);

            if (productItem == null)
            {
                return NotFound();
            }

            return Ok(productItem);
        }

        // PUT: api/ProductItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductItem([FromRoute] long id, [FromBody] ProductItem productItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(productItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ProductItems
        [HttpPost]
        public async Task<IActionResult> PostProductItem([FromBody] ProductItem productItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ProductItems.Add(productItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductItem", new { id = productItem.Id }, productItem);
        }

        // DELETE: api/ProductItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductItem([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productItem = await _context.ProductItems.SingleOrDefaultAsync(m => m.Id == id);
            if (productItem == null)
            {
                return NotFound();
            }

            _context.ProductItems.Remove(productItem);
            await _context.SaveChangesAsync();

            return Ok(productItem);
        }

        private bool ProductItemExists(long id)
        {
            return _context.ProductItems.Any(e => e.Id == id);
        }
    }
}