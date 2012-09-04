using System;
using System.Data.Entity;
using System.Data.Objects;


namespace Adventure.Data.Infrastructure
{
    public interface IUnitOfWork 
    {
        ObjectContext Context { get; set; }
        void Commit();
        bool LazyLoadingEnabled { get; set; }
        bool ProxyCreationEnabled { get; set; }
        string ConnectionString { get; set; }
    }
}