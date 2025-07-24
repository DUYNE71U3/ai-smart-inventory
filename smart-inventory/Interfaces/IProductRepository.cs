using smart_inventory.Models;

namespace smart_inventory.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsWithCategoryAsync();
        Task<Product?> GetProductWithCategoryAsync(int id);
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);
        Task<bool> IsSkuUniqueAsync(string sku, int? excludeId = null);
    }
}