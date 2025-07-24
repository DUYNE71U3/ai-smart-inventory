using MediatR;
using smart_inventory.DTOs;

namespace smart_inventory.CQRS.Products.Queries
{
    public class GetProductsByCategoryQuery : IRequest<IEnumerable<ProductDto>>
    {
        public int CategoryId { get; set; }
    }
}