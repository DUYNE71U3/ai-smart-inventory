using smart_inventory.Models;

namespace smart_inventory.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<Category>> GetCategoriesWithProductsAsync();
        Task<Category?> GetCategoryWithProductsAsync(int id);
        Task<bool> HasProductsAsync(int categoryId);
    }
}