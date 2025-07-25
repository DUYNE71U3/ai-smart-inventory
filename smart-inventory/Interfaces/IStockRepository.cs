using smart_inventory.Models;

namespace smart_inventory.Interfaces
{
    public interface IStockRepository : IGenericRepository<Stock>
    {
        Task<Stock?> GetStockWithDetailsAsync(int id);
        Task<bool> IsDocumentNoUniqueAsync(string documentNo);
        Task<IEnumerable<Stock>> GetAllWithDetailsAsync();
        Task<IEnumerable<Stock>> GetStocksByTypeAsync(StockType type);
        Task<decimal> GetTotalAmountAsync(int stockId);
        Task<Stock?> GetLatestStockByTypeAsync(StockType type);
    }
}