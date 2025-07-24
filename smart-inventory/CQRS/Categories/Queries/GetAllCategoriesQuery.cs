using MediatR;
using smart_inventory.DTOs;

namespace smart_inventory.CQRS.Categories.Queries
{
    public class GetAllCategoriesQuery : IRequest<IEnumerable<CategoryDto>>
    {
    }
}