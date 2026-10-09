using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;
using P1_OnlineShoppingWebAPI.Data;    // For AppDbContext (change to your actual namespace)
using P1_OnlineShoppingWebAPI.DTOs; 
using P1_OnlineShoppingWebAPI.Models;

//  REMEMBER THE SYNTAX FOR LINE 4-5!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!   
[ApiController]
[Route("api/[controller]")] // Route: api/products
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProductsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/products
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _context.Products.ToListAsync();
        return Ok(products);
    }

    // GET: api/products/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    // GET: api/products/search?category=books
    [HttpGet("search")]
    public IActionResult Search([FromQuery] string category)
    {
        return Ok(new { Category = category });
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDTO dto)
    {
        // 1. Automatic Validation
        // [ApiController] automatically returns 400 Bad Request if DTO DataAnnotations fail.
        // Manual fallback if needed: if (!ModelState.IsValid) return BadRequest(ModelState);

        // 2. Map DTO to Domain Entity
        var product = new Product
        {
            pName = dto.Name,
            pCategory = dto.Category,
            pPrice = dto.Price,
            pStockQuantity = dto.StockQuantity,
            pInStock = dto.InStock
        };

        // 3. Persistence
        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        // 4. Return 201 Created with 'Location' header pointing to GetById route
        return CreatedAtAction(
            actionName: nameof(GetById),
            routeValues: new { id = product.pId },
            value: product
        );
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchProduct(int id, [FromBody] JsonPatchDocument<UpdateProductDTO> patchDoc)
    {
        if (patchDoc == null)
        {
            return BadRequest();
        }

        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        // Map existing database entity to DTO
        var productToPatch = new UpdateProductDTO
        {
            Name = product.pName,
            Category = product.pCategory,
            Price = product.pPrice,
            StockQuantity = product.pStockQuantity,
            InStock = product.pInStock
        };

        // Apply operations from request body to DTO
        patchDoc.ApplyTo(productToPatch, ModelState);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Map updated DTO values back to the DB entity
        product.pName = productToPatch.Name;
        product.pCategory = productToPatch.Category;
        product.pPrice = productToPatch.Price;
        product.pStockQuantity = productToPatch.StockQuantity;
        product.pInStock = productToPatch.InStock;

        await _context.SaveChangesAsync();

        return NoContent(); // 204 No Content or 200 OK with updated entity
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductDTO dto)
    {
        // 1. ID Match Check
        if (id != dto.Id)
        {
            return BadRequest(new { message = "URL ID and Body ID mismatch." });
        }

        // 2. Locate Entity
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound(new { message = $"Product with ID {id} not found." });
        }

        // 3. Complete Replacement
        product.pName = dto.Name;
        product.pCategory = dto.Category;
        product.pPrice = dto.Price;
        product.pStockQuantity = dto.StockQuantity;
        product.pInStock = dto.InStock;

        // 4. Save & Respond
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.Products.AnyAsync(p => p.pId == id))
            {
                return NotFound();
            }
            throw;
        }

        return NoContent(); // 204 No Content
    }

    // DELETE: api/products/5 (Hard Delete Example)
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound(new { message = $"Product with ID {id} not found." });
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return NoContent(); // 204 No Content
    }
}