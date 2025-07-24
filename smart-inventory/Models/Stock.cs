using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace smart_inventory.Models
{
    public class Stock
    {
        public int Id { get; set; }

        [Required]
        public string DocumentNo { get; set; } = string.Empty; // Số chứng từ

        [Required]
        public StockType Type { get; set; } // Loại phiếu (Nhập/Xuất)

        [Required]
        public DateTime DocumentDate { get; set; } // Ngày chứng từ

        public string? Reference { get; set; } // Số tham chiếu (nếu có)

        [StringLength(1000)]
        public string? Notes { get; set; } // Ghi chú

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Navigation properties
        public virtual ICollection<StockDetail> Details { get; set; } = new List<StockDetail>();
    }

    public class StockDetail
    {
        public int Id { get; set; }

        public int StockId { get; set; }
        public virtual Stock Stock { get; set; } = null!;

        public int ProductId { get; set; }
        public virtual Product Product { get; set; } = null!;

        [Required]
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        public string? Notes { get; set; }
    }

    public enum StockType
    {
        [Display(Name = "Nhập kho")]
        StockIn = 1,

        [Display(Name = "Xuất kho")]
        StockOut = 2
    }
}