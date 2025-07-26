using MediatR;

namespace smart_inventory.CQRS.Suppliers.Commands
{
    public class DeleteSupplierCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}