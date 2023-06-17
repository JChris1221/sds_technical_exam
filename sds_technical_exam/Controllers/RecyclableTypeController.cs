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
            IEnumerable<RecyclableType> results = RecyclableTypeRepository.GetAll();

            if(results == null)
                return View(new List<RecyclableType>());
            
            return View(results.ToList());
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
                    TempData["SuccessMessage"] = "Record Succesfully Added";
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
        public ActionResult Edit(int id, RecyclableTypeViewModel rtv)
        {
            if (ModelState.IsValid)
            {
                rtv.RecyclableType.Id = id;
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
                catch (Exception e) {
                    ViewData["ErrorMessage"] = e.Message;
                    return View(rtv);
                }
            }
            else
            {
                return View(rtv);
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            RecyclableTypeViewModel rtv = new RecyclableTypeViewModel();
            rtv.RecyclableType = RecyclableTypeRepository.GetEntityById(id);
            if (rtv.RecyclableType != null)
                return View(rtv);
            else
                return new HttpNotFoundResult();
        }

        [HttpPost]
        public ActionResult Delete(int id, RecyclableTypeViewModel rtv)
        {
            int check = new RecyclableItemRepository().CheckTypeCount(id);
            if(check == 0)
            {
                if (RecyclableTypeRepository.Delete(id))
                {
                    TempData["SuccessMessage"] = "Record Deleted";
                }
                else
                {
                    if (RecyclableTypeRepository.ErrorLog.Exception != null)
                        TempData["ErrorMessage"] = RecyclableTypeRepository.ErrorLog.Exception.Message;
                    else
                        TempData["ErrorMessage"] = "Error Deleting Record";
                }
               
            }
            else
            {
                TempData["ErrorMessage"] = "Cannot Delete Record. (Some Recyclable Items uses this type. Please delete the items before deleting this type)";
            }
            return RedirectToAction("Index");
        }
    }
}