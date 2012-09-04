using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Adventure.Data.Infrastructure;
namespace Adventure.Data
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
        {
                public ProductRepository(IUnitOfWork unitofwork):base(unitofwork)
                { 
                }

        }
    public interface IProductRepository : IRepository<Product>
    {
    }
}