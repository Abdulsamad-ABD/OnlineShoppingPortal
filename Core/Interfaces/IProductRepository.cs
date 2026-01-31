using Core.Entities;

namespace Core.Interfaces;

public interface IProductRepository
{
    Task<IReadOnlyList<Product>> GetProductListAsync(string? type,string? brand,string? sort);
    Task<Product?> GetProductByIdAsync(int id);
    Task<IReadOnlyList<string>> GetTypesAsync();
    Task<IReadOnlyList<string>> GetBrandsAsync();
    void AddProduct(Product product);
    void UpdateProduct(Product product);
    void RemoveProduct(Product product);
    Task<bool> SaveChangesAsync();
    Task<bool> ProductExistsAsync(int id);
     
}