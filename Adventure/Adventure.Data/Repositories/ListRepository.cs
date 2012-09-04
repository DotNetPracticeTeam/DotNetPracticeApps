using System;
using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;
using Adventure.Data.Infrastructure;

namespace Adventure.Data
{
public class ListRepository<T>:IListRepository<T> where T:class
{

    public IUnitOfWork UnitOfWork { get; set; }
    public ListRepository(IUnitOfWork unitofwork)
    {
        UnitOfWork = unitofwork;    
    }

    
    private IObjectSet<T> _objectset;
    private ObjectContext _context;

    private IObjectSet<T> ObjectSet
    {
        get
        {
            if (_objectset == null)
            {
                _objectset = UnitOfWork.Context.CreateObjectSet<T>();
            }
            return _objectset;
        }
    }

    private ObjectContext context
    {
        get
        {
            if (_context == null)
            {
                _context = UnitOfWork.Context;
            }
            return _context;
        }
    }

    public virtual IQueryable<T> GetAll()
    {
        return ObjectSet.AsQueryable(); 
        
    }
    public virtual IQueryable<T> GetMany(Expression<Func<T, bool>> where)
    {
        return ObjectSet.Where(where).AsQueryable();
    }
    public T Get(Expression<Func<T, bool>> where)
    {

        return ObjectSet.AsQueryable().Where(where).FirstOrDefault();
    }



    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {

        if (!this.disposed)
            if (disposing)
            {
                if (_context != null)
                    _context.Dispose();
            }
        this.disposed = true;
    }

    public void Dispose()
    {

        Dispose(true);
        GC.SuppressFinalize(this);
    }



}
}
