using smart_inventory.DTOs;

namespace smart_inventory.ViewModels
{
    public class CategoryIndexViewModel
    {
        public IEnumerable<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
        public int TotalSuppliers { get; set; }
    }
}