using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adventure.Data.Interface;
using Adventure.Data;

namespace Adventure.Data.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
