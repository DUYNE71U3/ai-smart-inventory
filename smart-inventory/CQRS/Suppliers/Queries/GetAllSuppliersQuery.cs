using MediatR;
using smart_inventory.DTOs;

namespace smart_inventory.CQRS.Suppliers.Queries
{
    public class GetAllSuppliersQuery : IRequest<IEnumerable<SupplierDto>>
    {
    }
}