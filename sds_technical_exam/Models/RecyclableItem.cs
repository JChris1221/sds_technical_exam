using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using sds_technical_exam.Models;

namespace sds_technical_exam.Models
{
    public class RecyclableItem
    {
        public int Id { get; set; }
        //public RecyclableType
        public RecyclableType RecyclableType { get; set; }
        public int RecyclableTypeId { get; set; }
        public float Weight { get; set; }

        public float ComputedRate { get; set; }
        public string ItemDescription { get; set; }

        public RecyclableItem()
        {
            RecyclableType = new RecyclableType();
        }
    }
}