using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace smart_inventory.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string SKU { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string? Location { get; set; }
        public string? Notes { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }

    public class CreateProductDto
    {
        [Required(ErrorMessage = "Tên sản phẩm là bắt buộc")]
        [Display(Name = "Tên sản phẩm")]
        [StringLength(200, ErrorMessage = "Tên sản phẩm không được vượt quá 200 ký tự")]
        public string Name { get; set; } = string.Empty;
        
        [Display(Name = "Mô tả")]
        [StringLength(1000, ErrorMessage = "Mô tả không được vượt quá 1000 ký tự")]
        public string? Description { get; set; }
        
        [Required(ErrorMessage = "Mã SKU là bắt buộc")]
        [Display(Name = "Mã SKU")]
        [StringLength(50, ErrorMessage = "Mã SKU không được vượt quá 50 ký tự")]
        public string SKU { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Số lượng là bắt buộc")]
        [Display(Name = "Số lượng")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn hoặc bằng 0")]
        public int Quantity { get; set; }
        
        [Required(ErrorMessage = "Giá là bắt buộc")]
        [Display(Name = "Giá (VNĐ)")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Giá phải lớn hơn 0")]
        public decimal Price { get; set; }
        
        [Display(Name = "Vị trí kho")]
        [StringLength(100, ErrorMessage = "Vị trí kho không được vượt quá 100 ký tự")]
        public string? Location { get; set; }
        
        [Display(Name = "Ghi chú")]
        [StringLength(500, ErrorMessage = "Ghi chú không được vượt quá 500 ký tự")]
        public string? Notes { get; set; }
        
        [Required(ErrorMessage = "Danh mục là bắt buộc")]
        [Display(Name = "Danh mục")]
        public int CategoryId { get; set; }
    }

    public class UpdateProductDto
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Tên sản phẩm là bắt buộc")]
        [Display(Name = "Tên sản phẩm")]
        [StringLength(200, ErrorMessage = "Tên sản phẩm không được vượt quá 200 ký tự")]
        public string Name { get; set; } = string.Empty;
        
        [Display(Name = "Mô tả")]
        [StringLength(1000, ErrorMessage = "Mô tả không được vượt quá 1000 ký tự")]
        public string? Description { get; set; }
        
        [Required(ErrorMessage = "Mã SKU là bắt buộc")]
        [Display(Name = "Mã SKU")]
        [StringLength(50, ErrorMessage = "Mã SKU không được vượt quá 50 ký tự")]
        public string SKU { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Số lượng là bắt buộc")]
        [Display(Name = "Số lượng")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn hoặc bằng 0")]
        public int Quantity { get; set; }
        
        [Required(ErrorMessage = "Giá là bắt buộc")]
        [Display(Name = "Giá (VNĐ)")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Giá phải lớn hơn 0")]
        public decimal Price { get; set; }
        
        [Display(Name = "Vị trí kho")]
        [StringLength(100, ErrorMessage = "Vị trí kho không được vượt quá 100 ký tự")]
        public string? Location { get; set; }
        
        [Display(Name = "Ghi chú")]
        [StringLength(500, ErrorMessage = "Ghi chú không được vượt quá 500 ký tự")]
        public string? Notes { get; set; }
        
        [Required(ErrorMessage = "Danh mục là bắt buộc")]
        [Display(Name = "Danh mục")]
        public int CategoryId { get; set; }
        
        [Display(Name = "Trạng thái")]
        public bool IsActive { get; set; } = true;
    }
}