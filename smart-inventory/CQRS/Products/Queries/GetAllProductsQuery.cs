using MediatR;
using smart_inventory.DTOs;

namespace smart_inventory.CQRS.Products.Queries
{
    public class GetAllProductsQuery : IRequest<IEnumerable<ProductDto>>
    {
    }
}