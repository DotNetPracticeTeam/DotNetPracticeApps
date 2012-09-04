using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Adventure.Data.Infrastructure;

using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;

namespace Adventure.Data
{
    public class PurchaseRepository : RepositoryBase<PurchaseOrderHeader>, IPurchaseRepository
        {
        public PurchaseRepository(IUnitOfWork unitofwork):base(unitofwork)
        { 
        }

        public override void Update(PurchaseOrderHeader updatedObj)
        {

            //create a detail object OBJECTSET for deletion of objects
            IObjectSet<PurchaseOrderDetail> detailobj = UnitOfWork.Context.CreateObjectSet<PurchaseOrderDetail>();

            //get a existing header object to update
            PurchaseOrderHeader headerobj = ObjectSet.Where<PurchaseOrderHeader>(x => x.PurchaseOrderID == updatedObj.PurchaseOrderID).SingleOrDefault();

            //update header portion
            headerobj.RevisionNumber = updatedObj.RevisionNumber;
            headerobj.OrderDate = updatedObj.OrderDate.Date;
            headerobj.ShipDate = updatedObj.ShipDate;
            headerobj.ShipMethodID = updatedObj.ShipMethodID;
            headerobj.VendorID = updatedObj.VendorID;
            headerobj.Status = updatedObj.Status;
            headerobj.SubTotal = updatedObj.SubTotal;
            headerobj.TaxAmt = updatedObj.TaxAmt;
            headerobj.Freight = updatedObj.Freight;
            headerobj.ModifiedDate = updatedObj.ModifiedDate.Date;
                
            var count = headerobj.PurchaseOrderDetails.Count();
            for (int i = count-1; i >= 0; i--)
            {
                detailobj.DeleteObject(headerobj.PurchaseOrderDetails.ElementAt(i));  
            }

            count = updatedObj.PurchaseOrderDetails.Count();
            while(true)
            {
                headerobj.PurchaseOrderDetails.Add(updatedObj.PurchaseOrderDetails.ElementAt(count - 1)); 
              count--;
              if (count <= 0) break;
            }
              

              UnitOfWork.Commit();
          } 


           public override void Delete(Expression<Func<PurchaseOrderHeader, bool>> where) 
            {

                IObjectSet<PurchaseOrderDetail> detailobj = UnitOfWork.Context.CreateObjectSet<PurchaseOrderDetail>(); 
                PurchaseOrderHeader headerobj = ObjectSet.Where<PurchaseOrderHeader>(where).SingleOrDefault();
                var count = headerobj.PurchaseOrderDetails.Count();
                while(true)
                {
                    detailobj.DeleteObject(headerobj.PurchaseOrderDetails.ElementAt(count-1));
                    count--;
                    if (count <= 0) break;
                }
                ObjectSet.DeleteObject(headerobj);
                UnitOfWork.Commit();
            }

           public override IQueryable<PurchaseOrderHeader> GetAll()
           {
               return ObjectSet.OrderByDescending(x=>x.PurchaseOrderID).AsQueryable();
           }
        }
    public interface IPurchaseRepository : IRepository<PurchaseOrderHeader>
    {
    }
}