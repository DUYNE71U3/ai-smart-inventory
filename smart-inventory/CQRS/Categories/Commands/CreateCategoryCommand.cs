using MediatR;
using smart_inventory.DTOs;

namespace smart_inventory.CQRS.Categories.Commands
{
    public class CreateCategoryCommand : IRequest<CategoryDto>
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}