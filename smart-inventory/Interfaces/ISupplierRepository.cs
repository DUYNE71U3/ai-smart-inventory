using smart_inventory.Models;

namespace smart_inventory.Interfaces
{
    public interface ISupplierRepository : IGenericRepository<Supplier>
    {
        Task<bool> IsCodeUniqueAsync(string code, int? id = null);
        Task<Supplier?> GetSupplierWithProductsAsync(int id);
        Task<IEnumerable<Supplier>> GetAllWithProductCountAsync();
    }
}