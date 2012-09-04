using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pubs.Data.Interface;
using Pubs.Data;

namespace Pubs.Data.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
