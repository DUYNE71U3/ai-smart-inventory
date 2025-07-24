using MediatR;
using smart_inventory.DTOs;

namespace smart_inventory.CQRS.Products.Commands
{
    public class CreateProductCommand : IRequest<ProductDto>
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string SKU { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string? Location { get; set; }
        public string? Notes { get; set; }
        public int CategoryId { get; set; }
    }
}