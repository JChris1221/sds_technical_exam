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

        [HttpGet]
        public ActionResult Add()
        {

            if(new RecyclableTypeRepository().GetAll().ToList().Count() == 0)
            {
                TempData["ErrorMessage"] = "Cannot add recyclable items without recyclable types (Please add recyclable types)";
                return RedirectToAction("Index");
            }

            RecyclableItemViewModel riv = new RecyclableItemViewModel();
            riv.RecyclableItem = new RecyclableItem();
            riv = GetSelectItemsList(riv);

            return View(riv);
        }
        [HttpPost]
        public ActionResult Add(RecyclableItemViewModel riv)
        {

            if (ModelState.IsValid)
            {
                RecyclableType check = new RecyclableTypeRepository().GetEntityById(riv.RecyclableItem.RecyclableTypeId);
                if (riv.RecyclableItem.Weight >= check.MinKg && riv.RecyclableItem.Weight <= check.MaxKg)
                {
                    if (RecyclableItemRepository.Add(riv.RecyclableItem) > 0)
                    {
                        TempData["SuccessMessage"] = "Record Succesfully Added";
                        return RedirectToAction("Index");
                    }
                    
                }
                else
                {
                    TempData["ErrorMessage"] = "Invalid Weight. Must be between ("+check.MinKg+"KG and "+check.MaxKg+"KG).";       
                }
            }
            
            riv = GetSelectItemsList(riv);
            return View(riv);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            RecyclableItemViewModel riv = new RecyclableItemViewModel();
            riv.RecyclableItem = RecyclableItemRepository.GetEntityById(id);
            if(riv.RecyclableItem == null)
            {
                return new HttpNotFoundResult();
            }
            riv = GetSelectItemsList(riv);
            return View(riv);
        }

        [HttpPost]
        public ActionResult Edit(int id, RecyclableItemViewModel riv)
        {
            RecyclableType check = new RecyclableTypeRepository().GetEntityById(riv.RecyclableItem.RecyclableTypeId);
            if (riv.RecyclableItem.Weight >= check.MinKg && riv.RecyclableItem.Weight <= check.MaxKg)
            {
                if (ModelState.IsValid)
                {
                    riv.RecyclableItem.Id = id;
                    if (RecyclableItemRepository.Update(riv.RecyclableItem))
                    {
                        TempData["SuccessMessage"] = "Record Updated";
                    }
                    else
                        TempData["ErrorMessage"] = "Failed to Update Record";

                    return RedirectToAction("Index");
                }
            }
            else
                TempData["ErrorMessage"] = "Invalid Weight. Must be between (" + check.MinKg + "KG and " + check.MaxKg + "KG).";

            riv = GetSelectItemsList(riv);
            return View(riv);
            
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            RecyclableItemViewModel riv = new RecyclableItemViewModel();
            riv.RecyclableItem = RecyclableItemRepository.GetEntityById(id);
            if (riv.RecyclableItem != null)
            {
                riv = GetSelectItemsList(riv);
                return View(riv);
            }
            else
                return new HttpNotFoundResult();
        }

        public ActionResult Delete(int id, RecyclableTypeViewModel rtv)
        {
            if (RecyclableItemRepository.Delete(id))
            {
                TempData["SuccessMessage"] = "Record Deleted";
            }
            else
            {
                if (RecyclableItemRepository.ErrorLog.Exception != null)
                    TempData["ErrorMessage"] = RecyclableItemRepository.ErrorLog.Exception.Message;
                else
                    TempData["ErrorMessage"] = "Error Deleting Record";
            }
            return RedirectToAction("Index");
        }

        private RecyclableItemViewModel GetSelectItemsList(RecyclableItemViewModel recyclableItemViewModel)
        {
            recyclableItemViewModel.RecyclableTypes = RecyclableType.ConvertToSelectList(new RecyclableTypeRepository().GetAll().ToList());
            return recyclableItemViewModel;
        }
    }
}