using MediatR;
using smart_inventory.DTOs;

namespace smart_inventory.CQRS.Stocks.Queries
{
    public class GetStockByIdQuery : IRequest<StockDto>
    {
        public int Id { get; set; }
    }
}