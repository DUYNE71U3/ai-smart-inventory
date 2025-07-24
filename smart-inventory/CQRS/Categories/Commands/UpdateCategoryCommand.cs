using MediatR;
using smart_inventory.DTOs;

namespace smart_inventory.CQRS.Categories.Commands
{
    public class UpdateCategoryCommand : IRequest<CategoryDto>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}