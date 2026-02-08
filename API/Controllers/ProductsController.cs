using System.Threading.Tasks;
using API.RequestHelpers;
using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class ProductsController(IGenericRepository<Product> repo) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery]ProductSpecParams specParams)
    {
        var spec = new ProductSpecification(specParams);
        return await CreatePagedResult( repo, spec , specParams.PageIndex , specParams.PageSize);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await repo.GetByIdAsync(id);

        if (product==null) return NotFound();

        return product;
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        repo.Add(product);
        if(await repo.SaveAllAsync())
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
        
        if(! await repo.ExistsAsync(id))
             return NotFound("Product Not Exist.");

        repo.Update(product);
        await repo.SaveAllAsync();

        return NoContent();

    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Product>> DeleteProduct(int id)
    {
        var product = await repo.GetByIdAsync(id);

        if (product==null)
            return NotFound();
        
        repo.Remove(product);
        if(await repo.SaveAllAsync())
        {
           return NoContent(); 
        }

        return BadRequest("Problem in deleting Product");
    }

    [HttpGet("Types")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
    {
        var spec = new TypeListSpecification(); 
        return Ok(await repo.ListAsync(spec));
    }

    [HttpGet("Brands")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
    {
        var spec = new BrandListSpecification();
        return Ok(await repo.ListAsync(spec));
    }
    
}