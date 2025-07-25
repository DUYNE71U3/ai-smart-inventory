using System.ComponentModel.DataAnnotations;

namespace smart_inventory.Models.ViewModels
{
    public class StockViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số chứng từ")]
        [Display(Name = "Số chứng từ")]
        public string DocumentNo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng chọn loại chứng từ")]
        [Display(Name = "Loại chứng từ")]
        public StockType Type { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ngày chứng từ")]
        [Display(Name = "Ngày chứng từ")]
        [DataType(DataType.Date)]
        public DateTime DocumentDate { get; set; } = DateTime.Now;

        [Display(Name = "Số tham chiếu")]
        public string? Reference { get; set; }

        [Display(Name = "Ghi chú")]
        public string? Notes { get; set; }

        public List<StockDetailViewModel> Details { get; set; } = new List<StockDetailViewModel>();
    }

    public class StockDetailViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn sản phẩm")]
        [Display(Name = "Sản phẩm")]
        public int ProductId { get; set; }

        [Display(Name = "Tên sản phẩm")]
        public string? ProductName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số lượng")]
        [Display(Name = "Số lượng")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập đơn giá")]
        [Display(Name = "Đơn giá")]
        [Range(0, double.MaxValue, ErrorMessage = "Đơn giá không được âm")]
        public decimal UnitPrice { get; set; }

        [Display(Name = "Ghi chú")]
        public string? Notes { get; set; }
    }

    public class StockListViewModel
    {
        public int Id { get; set; }
        public string DocumentNo { get; set; } = string.Empty;
        public StockType Type { get; set; }
        public DateTime DocumentDate { get; set; }
        public string? Reference { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}