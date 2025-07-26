using MediatR;
using smart_inventory.DTOs;

namespace smart_inventory.CQRS.Suppliers.Queries
{
    public class GetSupplierByIdQuery : IRequest<SupplierDto?>
    {
        public int Id { get; set; }
    }
}