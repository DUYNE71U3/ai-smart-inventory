using MediatR;
using smart_inventory.DTOs;
using smart_inventory.Models;

namespace smart_inventory.CQRS.Stocks.Commands
{
    public class UpdateStockCommand : IRequest<StockDto>
    {
        public int Id { get; set; }
        public string DocumentNo { get; set; } = string.Empty;
        public StockType Type { get; set; }
        public DateTime DocumentDate { get; set; }
        public string? Reference { get; set; }
        public string? Notes { get; set; }
        public List<UpdateStockDetailCommand> Details { get; set; } = new List<UpdateStockDetailCommand>();
    }

    public class UpdateStockDetailCommand
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string? Notes { get; set; }
    }
}