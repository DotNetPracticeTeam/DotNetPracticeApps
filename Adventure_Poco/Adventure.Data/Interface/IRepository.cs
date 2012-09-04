using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Adventure.Data.Interface
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetQuery();

        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> where);
        T Single(Expression<Func<T, bool>> where);
        T First(Expression<Func<T, bool>> where);

        void Delete(Expression<Func<T, bool>> where);
        void Delete(T entity);
        void Update(T entity);
        void Add(T entity);
        void Attach(T entity);
        void SaveChanges();
    }
}
