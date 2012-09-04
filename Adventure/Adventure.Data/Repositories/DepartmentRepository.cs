using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Adventure.Data.Infrastructure;
namespace Adventure.Data
{
    public class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
        {
                public DepartmentRepository(IUnitOfWork unitofwork):base(unitofwork)
                { 
                }

        }
    public interface IDepartmentRepository : IRepository<Department>
    {
    }
}