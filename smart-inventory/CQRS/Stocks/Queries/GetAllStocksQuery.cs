using MediatR;
using smart_inventory.DTOs;

namespace smart_inventory.CQRS.Stocks.Queries
{
    public class GetAllStocksQuery : IRequest<IEnumerable<StockDto>>
    {
    }
}