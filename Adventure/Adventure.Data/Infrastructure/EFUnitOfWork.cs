using System.Data.Objects;
using System;


namespace Adventure.Data.Infrastructure
{
    public class EFUnitOfWork : IUnitOfWork, IDisposable
    {
        public ObjectContext Context { get; private set; }

        public EFUnitOfWork(ObjectContext context)
        {
            Context = context;
            context.ContextOptions.LazyLoadingEnabled = true;
        }

        public void Commit()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            if (Context != null)
            {
                Context.Dispose();
                Context = null;
            }

            GC.SuppressFinalize(this);
        }
    }

}
