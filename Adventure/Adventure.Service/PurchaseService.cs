using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adventure.Domain.Entities;
using Adventure.Data.Infrastructure;
using Adventure.Data;
using AutoMapper;
namespace Adventure.Service
{
    public interface IPurchaseService
    {
        IPurchaseHeader GetPurchaseOrder(int orderID);
        IQueryable<IPurchaseHeader> GetAll();
        void AddPurchaseOrder(IPurchaseHeader purchaseOrder);
        void UpdatePurchaseOrder(int orderID, IPurchaseHeader purchaseOrder);
        void DeletePurchaseOrder(int orderID);
        IQueryable<IMaster> GetProducts();
        IQueryable<IMaster> GetVendors();
        IQueryable<IMaster> GetShipMethods();
    }
    
    public class PurchaseService :IPurchaseService
    {
        private IPurchaseRepository _repository;
        private IProductRepository _productrepo;
        private IVendorRepository _vendorrepo;
        private IShipMethodRepository _shipmethodrepo;

        public PurchaseService(IPurchaseRepository repo, IProductRepository prodrepo, IVendorRepository vendrepo, IShipMethodRepository shiprepo)
        {
            
            _repository = repo;
            _productrepo = prodrepo;
            _vendorrepo = vendrepo;
            _shipmethodrepo = shiprepo;

            Mapper.CreateMap<PurchaseOrderHeader, IPurchaseHeader>();
            Mapper.CreateMap<IPurchaseHeader, PurchaseOrderHeader>();
            Mapper.CreateMap<PurchaseOrderDetail, IPurchaseDetail>();
            Mapper.CreateMap<IPurchaseDetail, PurchaseOrderDetail>();

            Mapper.CreateMap<PurchaseOrderHeader, PurchaseOrderHeader>();
            Mapper.CreateMap<PurchaseOrderDetail, PurchaseOrderDetail>();

            Mapper.CreateMap<Product, IMaster>();
            Mapper.CreateMap<Vendor,IMaster>();
            Mapper.CreateMap<ShipMethod, IMaster>();
        }




        public IPurchaseHeader GetPurchaseOrder(int orderID)
        {
            var purcOrder = _repository.Get(x=>x.PurchaseOrderID==orderID);
            var pocoOrder = Mapper.Map<PurchaseOrderHeader, IPurchaseHeader>(purcOrder);
            pocoOrder.purchaseDetail = Mapper.Map<List<PurchaseOrderDetail>, List<IPurchaseDetail>>(purcOrder.PurchaseOrderDetails.ToList()).AsQueryable();
            return pocoOrder;
        }

        public IQueryable<IPurchaseHeader> GetAll()
        {
            var purcOrders = _repository.GetAll();
            var pocoOrders = Mapper.Map<List<PurchaseOrderHeader>, List<IPurchaseHeader>>(purcOrders.ToList());
            return pocoOrders.AsQueryable();
        }

        public void AddPurchaseOrder(IPurchaseHeader purchaseOrder)
        {
            var newpurc = Mapper.Map<IPurchaseHeader, PurchaseOrderHeader>(purchaseOrder);
            var newpurcDetail = Mapper.Map<List<IPurchaseDetail>, List<PurchaseOrderDetail>>(purchaseOrder.purchaseDetail.ToList());
            foreach (var item in newpurcDetail)
            {
                newpurc.PurchaseOrderDetails.Add(item);
            }
            _repository.Add(newpurc);
        }

        public void UpdatePurchaseOrder(int orderID, IPurchaseHeader purchaseOrder)
        {
            var updatedHeader = Mapper.Map<IPurchaseHeader, PurchaseOrderHeader>(purchaseOrder);
            var newpurcDetail = Mapper.Map<List<IPurchaseDetail>, List<PurchaseOrderDetail>>(purchaseOrder.purchaseDetail.ToList());
            foreach (var item in newpurcDetail)
            {
                updatedHeader.PurchaseOrderDetails.Add(item);
            }

            _repository.Update(updatedHeader);
            
        }

        public void DeletePurchaseOrder(int orderID)
        {
            _repository.Delete(x=>x.PurchaseOrderID==orderID);
        }


        public IQueryable<IMaster> GetProducts()
        {
            var products = _productrepo.GetAll();
            var prods= products.Select(x => new Master { MasterID=x.ProductID, MasterName=x.Name }).AsQueryable();
            return prods;

        }

        public IQueryable<IMaster> GetVendors()
        {
            var vendors = _vendorrepo.GetAll();
            var vends = vendors.Select(x => new Master { MasterID = x.BusinessEntityID, MasterName = x.Name}).AsQueryable();
            return vends;
        }

        public IQueryable<IMaster> GetShipMethods()
        {
            var shipmethods = _shipmethodrepo.GetAll();
            var ships = shipmethods.Select(x => new Master { MasterID = x.ShipMethodID, MasterName = x.Name}).AsQueryable();
            return ships;
        }
    }
}
