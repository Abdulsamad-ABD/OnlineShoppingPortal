using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class ProductsController(IProductRepository repo) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts(string? type, string? brand, string? sort)
    {
        return Ok(await repo.GetProductListAsync(type,brand,sort));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await repo.GetProductByIdAsync(id);

        if (product==null) return NotFound();

        return product;
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        repo.AddProduct(product);
        if(await repo.SaveChangesAsync())
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
        
        if(! await repo.ProductExistsAsync(id))
             return NotFound("Product Not Exist.");

        repo.UpdateProduct(product);
        await repo.SaveChangesAsync();

        return NoContent();

    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Product>> DeleteProduct(int id)
    {
        var product = await repo.GetProductByIdAsync(id);

        if (product==null)
            return NotFound();
        
        repo.RemoveProduct(product);
        if(await repo.SaveChangesAsync())
        {
           return NoContent(); 
        }

        return BadRequest("Problem in deleting Product");
    }

    [HttpGet("Types")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
    {
        return Ok(await repo.GetTypesAsync());
    }

    [HttpGet("Brands")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
    {
        return Ok(await repo.GetBrandsAsync());
    }
    
}