using smart_inventory.DTOs;
using smart_inventory.CQRS.Stocks.Commands;

namespace smart_inventory.Interfaces
{
    public interface IStockService
    {
        Task<StockDto> CreateStockAsync(CreateStockCommand command);
        Task<StockDto> GetStockByIdAsync(int id);
        Task<IEnumerable<StockDto>> GetAllStocksAsync();
        Task<bool> DeleteStockAsync(int id);
    }
}