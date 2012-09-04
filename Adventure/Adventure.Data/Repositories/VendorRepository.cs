using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Adventure.Data.Infrastructure;
namespace Adventure.Data
{
    public class VendorRepository : RepositoryBase<Vendor>, IVendorRepository
        {
                public VendorRepository(IUnitOfWork unitofwork):base(unitofwork)
                { 
                }

        }
    public interface IVendorRepository : IRepository<Vendor>
    {
    }
}