using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Adventure.Data;
using Adventure.Data.Infrastructure;
using Adventure.Domain.Entities;
using Adventure.Service;
using Adventure.Web.ViewModel;
using AutoMapper;


namespace Adventure.Web.Controllers
{
    
    public class PurchaseController : Controller
    {
        //
        // GET: /Purchase/
        //private readonly IPurchaseService purcService;
        private readonly IPurchaseService purcService;


        public PurchaseController(IPurchaseService service)
        {
           purcService = service;
           Mapper.CreateMap<IPurchaseHeader, PurchaseHeaderModel>();
           Mapper.CreateMap<IPurchaseDetail, PurchaseDetailModel>();
           Mapper.CreateMap<PurchaseHeaderModel, IPurchaseHeader>();
           Mapper.CreateMap<PurchaseDetailModel, IPurchaseDetail>();
           Mapper.CreateMap<Master, MasterModel>();

            
            
        }
        
        public ActionResult Index()
        {
            var purcOrders = purcService.GetAll().Take(10);
            var model = Mapper.Map<List<IPurchaseHeader>, List<PurchaseHeaderModel>>(purcOrders.ToList());
            return View(model.AsQueryable());
        }

        //
        // GET: /Purchase/Create

        public ActionResult Create()
        {
            var header = new PurchaseHeaderModel();
            var detail = new List<PurchaseDetailModel>();
            header.purchasedetailmodel=detail;
            header.VendorList = new SelectList(purcService.GetVendors(), "MasterID", "MasterName");
            header.ShipMethodList = new SelectList(purcService.GetShipMethods(), "MasterID", "MasterName");

           // purcOrder.pro
            return View(header);
        }

        public ActionResult Edit(int id)
        {
            var purcOrder = purcService.GetPurchaseOrder(id);
            var products = purcService.GetProducts();
            var modelHeader = Mapper.Map<IPurchaseHeader, PurchaseHeaderModel>(purcOrder);
            var modelDetail = Mapper.Map<List<IPurchaseDetail>, List<PurchaseDetailModel>>(purcOrder.purchaseDetail.ToList());

            var modelProducts = Mapper.Map<List<IMaster>, List<MasterModel>>(products.ToList());

            modelHeader.purchasedetailmodel = modelDetail;
            modelHeader.VendorList = new SelectList(purcService.GetVendors(), "MasterID", "MasterName");
            modelHeader.ShipMethodList = new SelectList(purcService.GetShipMethods(), "MasterID", "MasterName");
            ViewBag.Title = "Edit";

            return View("Create",modelHeader);
        }

        //
        // POST: /Purchase/Create

        [HttpPost]
        public JsonResult Create(PurchaseHeaderModel model)
        {
            try
            {
                TryValidateModel(model);
                if (ModelState.IsValid)
                {

                    // Perform Update
                    if (model.PurchaseOrderID > 0)
                    {

                        var CurrentHeader = Mapper.Map<PurchaseHeaderModel, IPurchaseHeader>(model);
                        var CurrentDetail = Mapper.Map<List<PurchaseDetailModel>, List<IPurchaseDetail>>(model.purchasedetailmodel.ToList());
                        CurrentHeader.purchaseDetail = CurrentDetail.AsQueryable();

                        purcService.UpdatePurchaseOrder(model.PurchaseOrderID, CurrentHeader);
                    }
                    //Perform Save
                    else
                    {
                        var CurrentHeader = Mapper.Map<PurchaseHeaderModel, IPurchaseHeader>(model);
                        var CurrentDetail = Mapper.Map<List<PurchaseDetailModel>, List<IPurchaseDetail>>(model.purchasedetailmodel.ToList());
                        CurrentHeader.purchaseDetail = CurrentDetail.AsQueryable();
                        purcService.AddPurchaseOrder(CurrentHeader);
                    }

                    // If Sucess== 1 then Save/Update Successfull else there it has Exception
                    return Json(new { Success = 1, PurchaseOrderId = model.PurchaseOrderID, ex = "" },JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                // If Sucess== 0 then Unable to perform Save/Update Operation and send Exception to View as JSON
                return Json(new { Success = 0, ex = ex.Message.ToString() });
            }

            return Json(new { Success = 0, ex = new Exception("Unable to save").Message.ToString() }, JsonRequestBehavior.AllowGet);
        }


        //
        // GET: /Purchase/Details/5

        public ActionResult Details(int id)
        {
            var purcOrder = purcService.GetPurchaseOrder(id);
            var products = purcService.GetProducts();
            var modelHeader = Mapper.Map<IPurchaseHeader, PurchaseHeaderModel>(purcOrder);
            var modelDetail = Mapper.Map<List<IPurchaseDetail>, List<PurchaseDetailModel>>(purcOrder.purchaseDetail.ToList());

            var modelProducts = Mapper.Map<List<IMaster>, List<MasterModel>>(products.ToList());

            modelHeader.purchasedetailmodel = modelDetail;
            ViewBag.Title = "Details";
            // purcOrder.pro
            return View("Details", modelHeader);
        }

        [HttpPost, ActionName("Details")]
        public ActionResult DetailsEdit(int id)
        {
            return RedirectToAction("Edit",new {Id=id });
        }



        //
        // GET: /Purchase/Delete/5

        public ActionResult Delete(int id)
        {
            var purcOrder = purcService.GetPurchaseOrder(id);
            var products = purcService.GetProducts();
            var modelHeader = Mapper.Map<IPurchaseHeader, PurchaseHeaderModel>(purcOrder);
            var modelDetail = Mapper.Map<List<IPurchaseDetail>, List<PurchaseDetailModel>>(purcOrder.purchaseDetail.ToList());

            var modelProducts = Mapper.Map<List<IMaster>, List<MasterModel>>(products.ToList());

            modelHeader.purchasedetailmodel = modelDetail;
            ViewBag.Title = "Delete";
            // purcOrder.pro
            return View("Delete", modelHeader);
        }

        //
        // POST: /Purchase/Delete/5

       

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            purcService.DeletePurchaseOrder(id);
            return RedirectToAction("Index");
        }



      /*  protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = Json("...");
            }
            else
            {
                base.OnException(filterContext);
            }
        } 
        */



    }
}
