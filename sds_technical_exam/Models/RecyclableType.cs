using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

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

        public static List<SelectListItem> ConvertToSelectList(List<RecyclableType> list)
        {
            List<SelectListItem> listItems = new List<SelectListItem>();

            foreach (RecyclableType r in list)
            {
                listItems.Add(new SelectListItem
                {
                    Text = r.Type,
                    Value = r.Id.ToString(),
                    Selected = false,
                });
            }
            return listItems;
        }
    }
}