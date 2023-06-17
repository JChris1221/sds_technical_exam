using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sds_technical_exam.Models;
using sds_technical_exam.ViewModels;
using sds_technical_exam.Data;

namespace sds_technical_exam.Controllers
{
    public class RecyclableItemController : Controller
    {
        // GET: RecyclableItem
        RecyclableItemRepository RecyclableItemRepository = new RecyclableItemRepository();
        public ActionResult Index()
        {
            IEnumerable<RecyclableItem> results = RecyclableItemRepository.GetAll();

            if (results == null)
                return View(new List<RecyclableItem>());

            return View(results.ToList());
        }
    }
}