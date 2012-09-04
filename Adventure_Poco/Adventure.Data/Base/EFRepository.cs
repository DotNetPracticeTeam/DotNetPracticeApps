using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Data.Objects;
using Adventure.Data.Interface;

namespace Adventure.Data.Base
{
    public class EFRepository<T> : IRepository<T> where T : class
    {
        private ObjectContext _context;
        private IObjectSet<T> _objectSet;

        protected ObjectContext Context
        {
            get
            {
                if (_context == null)
                {
                    _context = GetCurrentUnitOfWork<EFUnitOfWork>().Context;
                }

                return _context;
            }
        }

        protected IObjectSet<T> ObjectSet
        {
            get
            {
                if (_objectSet == null)
                {
                    _objectSet = this.Context.CreateObjectSet<T>();
                }

                return _objectSet;
            }
        }

        public TUnitOfWork GetCurrentUnitOfWork<TUnitOfWork>() where TUnitOfWork : IUnitOfWork
        {
            return (TUnitOfWork)UnitOfWork.Current;
        }

        public IQueryable<T> GetQuery()
        {
            return ObjectSet;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return GetQuery().ToList();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> where)
        {
            return this.ObjectSet.Where<T>(where);
        }

        public T Single(Expression<Func<T, bool>> where)
        {
            return this.ObjectSet.SingleOrDefault<T>(where);
        }

        public T First(Expression<Func<T, bool>> where)
        {
            return this.ObjectSet.First<T>(where);
        }

        public virtual void Delete(T entity)
        {
            this.ObjectSet.DeleteObject(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {

            IQueryable<T> objects = ObjectSet.Where<T>(where).AsQueryable();
            foreach (T obj in objects)
                ObjectSet.DeleteObject(obj);

        } 

        public virtual void Add(T entity)
        {
            this.ObjectSet.AddObject(entity);
        }

        public virtual void Update(T entity)
        {
            //this.ObjectSet(entity);
           // SaveChanges();
        }

        public void Attach(T entity)
        {
            this.ObjectSet.Attach(entity);
        }

        public void SaveChanges()
        {
            this.Context.SaveChanges();
        }
    }
}
