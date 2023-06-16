using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sds_technical_exam.Models
{
    public class RecyclableType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public float Rate { get; set; }
        public float MinKg { get; set; }
        public float MaxKg { get; set; }
    }
}