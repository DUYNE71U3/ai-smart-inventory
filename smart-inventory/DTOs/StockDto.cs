using smart_inventory.Models;

namespace smart_inventory.DTOs
{
    public class StockDto
    {
        public int Id { get; set; }
        public string DocumentNo { get; set; } = string.Empty;
        public StockType Type { get; set; }
        public string TypeName { get; set; } = string.Empty;
        public DateTime DocumentDate { get; set; }
        public string? Reference { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public decimal TotalAmount { get; set; }
        public List<StockDetailDto> Details { get; set; } = new List<StockDetailDto>();
    }

    public class StockDetailDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductSKU { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public string? Notes { get; set; }
    }
}