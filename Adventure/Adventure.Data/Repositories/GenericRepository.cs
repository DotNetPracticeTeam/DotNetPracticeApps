using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Adventure.Data.Infrastructure;

namespace Adventure.Data
{
    public class GenericRepository<T> : RepositoryBase<T> where T : class
        {
            public GenericRepository(IUnitOfWork unitofwork):base(unitofwork)
            { 
            }

        }
    
}