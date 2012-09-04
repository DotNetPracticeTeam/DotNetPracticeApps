using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Data;


namespace Adventure.Data.Infrastructure
{
public abstract class RepositoryBase<T>:IRepository<T> where T : class
{

    public IUnitOfWork UnitOfWork { get; set; }
    public RepositoryBase(IUnitOfWork unitofwork )
    {
        UnitOfWork = unitofwork;    
    }

    
    private IObjectSet<T> _objectset;
    private ObjectContext _context;

    protected IObjectSet<T> ObjectSet
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

    protected ObjectContext context
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

    public virtual void Add(T entity)
    {
        ObjectSet.AddObject(entity);
        UnitOfWork.Commit();
    }

    public virtual void Update(T entity)
    {
      //  ObjectSet.Attach(entity);
       // UnitOfWork.Context.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified); 

        UnitOfWork.Commit();
    }

    public virtual void Delete(T entity)
    {
        ObjectSet.DeleteObject(entity);
        UnitOfWork.Commit();
    }

    public virtual void Delete(Expression<Func<T, bool>> where) 
    {

        IQueryable<T> objects = ObjectSet.Where<T>(where).AsQueryable();
        foreach (T obj in objects)
            ObjectSet.DeleteObject(obj);

        UnitOfWork.Commit();
    } 
/*    public virtual T GetById(long id)
    {
        return ObjectSet.Find(id);
    }
    public virtual T GetById(string id)
    {
        return dbset.Find(id);
    }
  */
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
        return ObjectSet.Where(where).FirstOrDefault<T>();
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
