using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Adventure.Data.Infrastructure;
namespace Adventure.Data
{
    public class ShipMethodRepository : RepositoryBase<ShipMethod>, IShipMethodRepository
        {
                public ShipMethodRepository(IUnitOfWork unitofwork):base(unitofwork)
                { 
                }

        }
    public interface IShipMethodRepository : IRepository<ShipMethod>
    {
    }
}