using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Adventure.Data.Infrastructure
{
    public interface IRepository<T>:IDisposable where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> where);
        /*T GetById(long Id);
        T GetById(string Id);*/
        T Get(Expression<Func<T, bool>> where);
        IQueryable<T> GetAll();
        IQueryable<T> GetMany(Expression<Func<T, bool>> where);    
   
    }


    public interface IListRepository<T>: IDisposable where T:class
    {
    /*  void Add<T>(T entity);
        void Update<T>(T entity);
        void Delete<T>(T entity);
        void Delete<T>(Expression<Func<T, bool>> where);
        /*T GetById(long Id);
        T GetById(string Id);*/

        T Get(Expression<Func<T, bool>> where);
        IQueryable<T> GetAll();
        IQueryable<T> GetMany(Expression<Func<T, bool>> where);

    }

}
