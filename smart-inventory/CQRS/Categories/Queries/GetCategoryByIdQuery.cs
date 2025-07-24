using MediatR;
using smart_inventory.DTOs;

namespace smart_inventory.CQRS.Categories.Queries
{
    public class GetCategoryByIdQuery : IRequest<CategoryDto?>
    {
        public int Id { get; set; }
    }
}