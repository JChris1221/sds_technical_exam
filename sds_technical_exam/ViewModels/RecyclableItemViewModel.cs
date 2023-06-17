using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace sds_technical_exam.ViewModels
{
    public class RecyclableItemViewModel
    {
        public Models.RecyclableItem RecyclableItem { get; set; }
        public List<SelectListItem> RecyclableTypes { get; set; }
    }
}