using MediatR;
using smart_inventory.DTOs;
using smart_inventory.Models;

namespace smart_inventory.CQRS.Stocks.Commands
{
    public class CreateStockCommand : IRequest<StockDto>  
    {
        public string DocumentNo { get; set; } = string.Empty;
        public StockType Type { get; set; }
        public DateTime DocumentDate { get; set; }
        public string? Reference { get; set; }
        public string? Notes { get; set; }
        public List<CreateStockDetailCommand> Details { get; set; } = new List<CreateStockDetailCommand>();
    }

    public class CreateStockDetailCommand
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string? Notes { get; set; }
    }
}