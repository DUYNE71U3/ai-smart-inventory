using System.ComponentModel.DataAnnotations;

namespace smart_inventory.DTOs
{
    public class SupplierDto
    {
        public int Id { get; set; }

        [Display(Name = "Tên nhà cung cấp")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Mã nhà cung cấp")]
        public string Code { get; set; } = string.Empty;

        [Display(Name = "Địa chỉ")]
        public string? Address { get; set; }

        [Display(Name = "Số điện thoại")]
        public string? Phone { get; set; }

        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Display(Name = "Ghi chú")]
        public string? Notes { get; set; }

        [Display(Name = "Trạng thái")]
        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int ProductCount { get; set; }
    }

    public class CreateSupplierDto
    {
        [Required(ErrorMessage = "Tên nhà cung cấp là bắt buộc")]
        [Display(Name = "Tên nhà cung cấp")]
        [StringLength(200, ErrorMessage = "Tên nhà cung cấp không được vượt quá 200 ký tự")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mã nhà cung cấp là bắt buộc")]
        [Display(Name = "Mã nhà cung cấp")]
        [StringLength(50, ErrorMessage = "Mã nhà cung cấp không được vượt quá 50 ký tự")]
        public string Code { get; set; } = string.Empty;

        [Display(Name = "Địa chỉ")]
        [StringLength(500, ErrorMessage = "Địa chỉ không được vượt quá 500 ký tự")]
        public string? Address { get; set; }

        [Display(Name = "Số điện thoại")]
        [StringLength(20, ErrorMessage = "Số điện thoại không được vượt quá 20 ký tự")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Số điện thoại chỉ được chứa số")]
        public string? Phone { get; set; }

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [StringLength(100, ErrorMessage = "Email không được vượt quá 100 ký tự")]
        public string? Email { get; set; }

        [Display(Name = "Ghi chú")]
        [StringLength(1000, ErrorMessage = "Ghi chú không được vượt quá 1000 ký tự")]
        public string? Notes { get; set; }
    }

    public class UpdateSupplierDto : CreateSupplierDto
    {
        public int Id { get; set; }
        
        [Display(Name = "Trạng thái")]
        public bool IsActive { get; set; } = true;
    }
}