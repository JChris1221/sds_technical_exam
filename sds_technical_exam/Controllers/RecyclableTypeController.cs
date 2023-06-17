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

        [HttpPost]
        public ActionResult Add(RecyclableTypeViewModel rtv)
        {
            if (ModelState.IsValid)
            {
                if (RecyclableTypeRepository.Add(rtv.RecyclableType) != 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(rtv);
                }
            }
            else
            {
                return View(rtv);
            }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            RecyclableTypeViewModel rtv = new RecyclableTypeViewModel();
            rtv.RecyclableType = RecyclableTypeRepository.GetEntityById(id);
            if (rtv.RecyclableType != null)
                return View(rtv);
            else
                return new HttpNotFoundResult();
        }

        [HttpPost]
        public ActionResult Edit(RecyclableTypeViewModel rtv)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (RecyclableTypeRepository.Update(rtv.RecyclableType))
                    {
                        ViewData["SuccessMessage"] = "Recyclable Type Updated";
                        return RedirectToAction("Index");
                    }
                    ViewData["ErrorMessage"] = "Error Updating Recyclable Type";
                    return View(rtv);
                }
                catch (Exception e){
                    ViewData["ErrorMessage"] = e.Message;
                    return View(rtv);
                }
            }
            else
            {
                return View(rtv);
            }
        }
    }
}