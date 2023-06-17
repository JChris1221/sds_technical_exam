using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using sds_technical_exam.Models;

namespace sds_technical_exam.Models
{
    public class RecyclableItem
    {
        [Required]
        public int Id { get; set; }
        
        public RecyclableType RecyclableType { get; set; }
        [Required]
        public int RecyclableTypeId { get; set; }
        [Required]
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