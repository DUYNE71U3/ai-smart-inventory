using MediatR;

namespace smart_inventory.CQRS.Stocks.Commands
{
    public class DeleteStockCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}