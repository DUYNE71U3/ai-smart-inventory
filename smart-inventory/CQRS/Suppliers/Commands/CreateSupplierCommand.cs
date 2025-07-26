using MediatR;
using smart_inventory.DTOs;

namespace smart_inventory.CQRS.Suppliers.Commands
{
    public class CreateSupplierCommand : IRequest<SupplierDto>
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Notes { get; set; }
    }
}