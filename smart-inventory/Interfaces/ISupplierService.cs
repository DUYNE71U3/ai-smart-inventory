using smart_inventory.DTOs;

namespace smart_inventory.Interfaces
{
    public interface ISupplierService
    {
        Task<IEnumerable<SupplierDto>> GetAllSuppliersAsync();
        Task<SupplierDto?> GetSupplierByIdAsync(int id);
        Task<SupplierDto> CreateSupplierAsync(CreateSupplierDto createSupplierDto);
        Task<SupplierDto> UpdateSupplierAsync(UpdateSupplierDto updateSupplierDto);
        Task<bool> DeleteSupplierAsync(int id);
        Task<SupplierDto?> GetSupplierWithProductsAsync(int id);
    }
}