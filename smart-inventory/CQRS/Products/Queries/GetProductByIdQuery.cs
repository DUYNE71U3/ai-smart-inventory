using MediatR;
using smart_inventory.DTOs;

namespace smart_inventory.CQRS.Products.Queries
{
    public class GetProductByIdQuery : IRequest<ProductDto?>
    {
        public int Id { get; set; }
    }
}