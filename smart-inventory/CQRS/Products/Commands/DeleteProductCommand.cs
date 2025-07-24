using MediatR;

namespace smart_inventory.CQRS.Products.Commands
{
    public class DeleteProductCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}