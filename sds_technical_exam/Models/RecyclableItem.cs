using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace sds_technical_exam.Models
{
    public class RecyclableItem
    {
        [Required]
        public int Id { get; set; }
        public RecyclableType RecyclableType { get; set; }
        [Required]
        [DisplayName("Recyclable Type")]
        public int RecyclableTypeId { get; set; }
        [Required]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Invalid Decimal Format")]
        public float Weight { get; set; }
        [Required]
        public float ComputedRate { get; set; }
        [Required]
        [StringLength(150)]
        public string ItemDescription { get; set; }

        public RecyclableItem()
        {
            RecyclableType = new RecyclableType();
        }
    }
}