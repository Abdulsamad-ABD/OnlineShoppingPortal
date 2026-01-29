using System.Threading.Tasks;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class ProductsController(StoreContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        return await context.Products.ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await context.Products.FindAsync(id);

        if (product==null) return NotFound();

        return product;
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        context.Products.Add(product);
        if(await context.SaveChangesAsync()>0)
        {
            return CreatedAtAction("GetProduct", new {id = product.Id}, product);
        }
        return BadRequest("Issue in Product Creation");
    }
    
    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateProduct(int id,Product product)
    {
        if(id!=product.Id)
            return BadRequest("Id Mismatch");
        
        if(!ProductExist(id))
             return NotFound("Product Not Exist.");

        context.Entry(product).State = EntityState.Modified;
        await context.SaveChangesAsync();

        return NoContent();

    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Product>> DeleteProduct(int id)
    {
        var product = await context.Products.FindAsync(id);

        if (product==null)
            return NotFound();
        
        context.Products.Remove(product);
        if(await context.SaveChangesAsync()>0)
        {
           return NoContent(); 
        }

        return BadRequest("Problem in deleting Product");
    }
    
    
    public bool ProductExist(int id)
    {
        return context.Products.Any(x=>id==x.Id);
    }
}