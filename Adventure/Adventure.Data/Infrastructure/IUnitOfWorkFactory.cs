using System;
using System.Data.Entity;
using System.Data.Objects;


namespace Adventure.Data.Infrastructure
{

    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }

}