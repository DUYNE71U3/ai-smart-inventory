using Microsoft.EntityFrameworkCore;
using smart_inventory.Data;
using smart_inventory.Interfaces;
using smart_inventory.Models;

namespace smart_inventory.Repositories
{
    public class SupplierRepository : GenericRepository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> IsCodeUniqueAsync(string code, int? id = null)
        {
            if (id.HasValue)
            {
                return !await _context.Suppliers
                    .AnyAsync(s => s.Code == code && s.Id != id);
            }
            return !await _context.Suppliers
                .AnyAsync(s => s.Code == code);
        }

        public async Task<Supplier?> GetSupplierWithProductsAsync(int id)
        {
            return await _context.Suppliers
                .Include(s => s.Products)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Supplier>> GetAllWithProductCountAsync()
        {
            return await _context.Suppliers
                .Include(s => s.Products)
                .Select(s => new Supplier
                {
                    Id = s.Id,
                    Name = s.Name,
                    Code = s.Code,
                    Address = s.Address,
                    Phone = s.Phone,
                    Email = s.Email,
                    Notes = s.Notes,
                    IsActive = s.IsActive,
                    CreatedAt = s.CreatedAt,
                    UpdatedAt = s.UpdatedAt,
                    Products = s.Products
                })
                .ToListAsync();
        }
    }
}