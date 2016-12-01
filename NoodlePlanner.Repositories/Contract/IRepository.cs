using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace NoodlePlanner.Repositories.Contract
{
    public interface IRepository<T> where T : class
    {
        Database Database { get; }
        T FindById(object id);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        void InsertGraph(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Insert(T entity);
        T Detach(T entity);
        
    }
}
