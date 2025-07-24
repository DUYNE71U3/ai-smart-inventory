using smart_inventory.DTOs;

namespace smart_inventory.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalCategories { get; set; }
        public int TotalProducts { get; set; }
        public int ActiveProducts { get; set; }
        public int InactiveProducts { get; set; }
        public decimal TotalInventoryValue { get; set; }
        public List<ProductDto> LowStockProducts { get; set; } = new();
        public List<ProductDto> OutOfStockProducts { get; set; } = new();
        public List<CategoryDto> Categories { get; set; } = new();
        public List<ProductDto> RecentProducts { get; set; } = new();
    }
}