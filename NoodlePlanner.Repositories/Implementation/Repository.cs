using NoodlePlanner.DBContext.Contract;
using NoodlePlanner.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NoodlePlanner.Repositories.Implementation
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private IDbContext _ctx;
        private IDbSet<T> _dbSet;
        public Database Database
        {
            get
            {
                return _ctx.Database;
            }
        }

        public Repository(IDbContext context)
        {
            _dbSet = context.Set<T>();
            _ctx = context;
        }
        public virtual T FindById(object id)
        {
            return _dbSet.Find(id);
        }
        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _dbSet.Where(predicate);
            return query;
        }
        public virtual void InsertGraph(T entity)
        {
            _dbSet.Add(entity);
        }
        public virtual void Update(T entity)
        {
            _dbSet.Attach(entity);
            _ctx.ContextEntry(entity).State = EntityState.Modified;
        }
        public virtual void Delete(object id)
        {
            var entity = _dbSet.Find(id);
            _ctx.ContextEntry(entity).State = EntityState.Deleted;
            Delete(entity);
        }
        public virtual void Delete(T entity)
        {
            _dbSet.Attach(entity);
            _dbSet.Remove(entity);
        }
        public virtual void Insert(T entity)
        {
            _dbSet.Attach(entity);
            _ctx.ContextEntry(entity).State = EntityState.Added;
        }
        public virtual T Detach(T entity)
        {
            _ctx.ContextEntry(entity).State = EntityState.Detached;
            return entity;
        }
        public virtual RepositoryQuery<T> Query()
        {
            var repositoryGetFluentHelper =
                new RepositoryQuery<T>(this);

            return repositoryGetFluentHelper;
        }
        internal IQueryable<T> Get(Expression<Func<T, bool>> filter = null,
                                         Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                         List<Expression<Func<T, object>>> includeProperties = null,
                                         List<string> includePropertiesString = null,
                                         int? page = null,
                                         int? pageSize = null)
        {
            IQueryable<T> query = _dbSet;

            if (includeProperties != null)
                includeProperties.ForEach(i => { query = query.Include(i); });

            if (includePropertiesString != null)
                includePropertiesString.ForEach(i => { query = query.Include(i); });

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            if (page != null && pageSize != null)
                query = query
                    .Skip((page.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);

            return query;
        }
    }

}
