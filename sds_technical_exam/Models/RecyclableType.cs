using System.ComponentModel.DataAnnotations;

namespace sds_technical_exam.Models
{
    public class RecyclableType
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Field Exceeds Maximum Length")]
        public string Type { get; set; }
        [Required]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Invalid Decimal Format")]
        public float Rate { get; set; }
        [Required]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Invalid Decimal Format")]
        public float MinKg { get; set; }
        [Required]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Invalid Decimal Format")]
        public float MaxKg { get; set; }
    }
}