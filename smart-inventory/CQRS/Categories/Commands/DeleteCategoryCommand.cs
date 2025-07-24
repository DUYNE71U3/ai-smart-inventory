using MediatR;

namespace smart_inventory.CQRS.Categories.Commands
{
    public class DeleteCategoryCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}