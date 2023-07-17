using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesInformationSystem.Models
{
    public class SaleDetail
    {
        [Key]
        public long SaleDetailId { get; set; }
        public long? SaleId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        [ForeignKey("SaleId")]
        public virtual SaleMaster SaleMaster { get; set; }
    }
}
