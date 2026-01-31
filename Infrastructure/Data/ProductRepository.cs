using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Data;

public class ProductRepository(StoreContext context) : IProductRepository
{
    public void AddProduct(Product product)
    {
        context.Products.Add(product);
    }

    public async Task<IReadOnlyList<string>> GetBrandsAsync()
    {
        return await context.Products.Select(x=> x.Brand)
                        .Distinct( )
                        .ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
       return await context.Products.FindAsync(id);
    }

    public async Task<IReadOnlyList<Product>> GetProductListAsync(string? type, string? brand, string? sort)
    {
        var query = context.Products.AsQueryable();

        if(!string.IsNullOrWhiteSpace(type))
            query = query.Where(x=>x.Type == type);

        if(!string.IsNullOrWhiteSpace(brand))
           query = query.Where(x=>x.Brand == brand);

        query = sort switch
        {
            "priceAsc"  => query.OrderBy(x=>x.Price),
            "priceDesc" => query.OrderByDescending(x=>x.Price),
            _           => query.OrderBy(x=>x.Name)
        };

        return await query.ToListAsync();
    }

    public async Task<IReadOnlyList<string>> GetTypesAsync()
    {
        return await context.Products.Select(x=>x.Type)
        .Distinct()
        .ToListAsync();
    }

    public async Task<bool> ProductExistsAsync(int id)
    {
        return await context.Products.AnyAsync(x=>x.Id == id);
    }

    public void RemoveProduct(Product product)
    {
        context.Products.Remove(product);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await context.SaveChangesAsync()>0;
    }

    public void UpdateProduct(Product product)
    {
        context.Entry(product).State = EntityState.Modified; 
    }
}