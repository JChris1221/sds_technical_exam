using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sds_technical_exam.Models;
using sds_technical_exam.Data;
using sds_technical_exam.ViewModels;

namespace sds_technical_exam.Controllers
{
    public class RecyclableTypeController : Controller
    {
        RecyclableTypeRepository RecyclableTypeRepository = new RecyclableTypeRepository();
        public ActionResult Index()
        {
            List<RecyclableType> recyclableTypesList;
            recyclableTypesList = RecyclableTypeRepository.GetAll().ToList();
            return View(recyclableTypesList);
        }

        [HttpGet]
        public ActionResult Add()
        {
            RecyclableTypeViewModel rtv = new RecyclableTypeViewModel();
            rtv.RecyclableType = new RecyclableType();
            return View(rtv);
        }
    }
}