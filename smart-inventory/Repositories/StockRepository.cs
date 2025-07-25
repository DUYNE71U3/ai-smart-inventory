using Microsoft.EntityFrameworkCore;
using smart_inventory.Data;
using smart_inventory.Interfaces;
using smart_inventory.Models;

namespace smart_inventory.Repositories
{
    public class StockRepository : GenericRepository<Stock>, IStockRepository
    {
        public StockRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Stock?> GetStockWithDetailsAsync(int id)
        {
            return await _context.Stocks
                .Include(s => s.Details)
                    .ThenInclude(d => d.Product)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<bool> IsDocumentNoUniqueAsync(string documentNo)
        {
            return !await _context.Stocks.AnyAsync(s => s.DocumentNo == documentNo);
        }

        public async Task<IEnumerable<Stock>> GetAllWithDetailsAsync()
        {
            return await _context.Stocks
                .Include(s => s.Details)
                    .ThenInclude(d => d.Product)
                .OrderByDescending(s => s.DocumentDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Stock>> GetStocksByTypeAsync(StockType type)
        {
            return await _context.Stocks
                .Include(s => s.Details)
                    .ThenInclude(d => d.Product)
                .Where(s => s.Type == type)
                .OrderByDescending(s => s.DocumentDate)
                .ToListAsync();
        }

        public async Task<decimal> GetTotalAmountAsync(int stockId)
        {
            var stock = await _context.Stocks
                .Include(s => s.Details)
                .FirstOrDefaultAsync(s => s.Id == stockId);

            if (stock == null) return 0;

            return stock.Details.Sum(d => d.Quantity * d.UnitPrice);
        }

        public async Task<Stock?> GetLatestStockByTypeAsync(StockType type)
        {
            return await _context.Stocks
                .Where(s => s.Type == type)
                .OrderByDescending(s => s.DocumentDate)
                .FirstOrDefaultAsync();
        }
    }
}