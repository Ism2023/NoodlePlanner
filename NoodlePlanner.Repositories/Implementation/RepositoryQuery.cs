using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NoodlePlanner.Repositories.Implementation
{
    public sealed class RepositoryQuery<T> where T : class
    {
        private readonly List<Expression<Func<T, object>>> _includeProperties;
        private readonly List<string> _includePropertiesStrings;
        private readonly Repository<T> _repository;
        private Expression<Func<T, bool>> _filter;
        private Func<IQueryable<T>, IOrderedQueryable<T>> _orderByQuerable;
        private int? _page;
        private int? _pageSize;
        public RepositoryQuery(Repository<T> repository)
        {
            _repository = repository;
            _includeProperties = new List<Expression<Func<T, object>>>();
            _includePropertiesStrings = new List<string>();
        }
        public RepositoryQuery<T> Filter(Expression<Func<T, bool>> filter)
        {
            _filter = filter;
            return this;
        }
        public RepositoryQuery<T> OrderBy(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy)
        {
            _orderByQuerable = orderBy;
            return this;
        }
        public RepositoryQuery<T> Include(Expression<Func<T, object>> expression)
        {
            _includeProperties.Add(expression);
            return this;
        }
        public RepositoryQuery<T> Include(string expression)
        {
            _includePropertiesStrings.Add(expression);
            return this;
        }
        public IQueryable<T> GetPage(int page, int pageSize, out int totalCount)
        {
            _page = page;
            _pageSize = pageSize;
            totalCount = _repository.Get(_filter).Count();

            return _repository.Get(_filter, _orderByQuerable, _includeProperties, _includePropertiesStrings, _page, _pageSize);
        }
        public IQueryable<T> Get()
        {
            return _repository.Get(_filter, _orderByQuerable, _includeProperties, _includePropertiesStrings, _page, _pageSize);
        }

        public T Single()
        {
            return _repository.Get(_filter, _orderByQuerable, _includeProperties, _includePropertiesStrings, _page, _pageSize).Single();
        }
        public async Task<T> SingleAsync()
        {
            return await _repository.Get(_filter, _orderByQuerable, _includeProperties, _includePropertiesStrings, _page, _pageSize).SingleAsync();
        }
        public async Task<T> SingleOrDefaultAsync()
        {
            return await _repository.Get(_filter, _orderByQuerable, _includeProperties, _includePropertiesStrings, _page, _pageSize).SingleOrDefaultAsync();
        }

        public T FirstOrDefault()
        {
            return _repository.Get(_filter, _orderByQuerable, _includeProperties, _includePropertiesStrings, _page, _pageSize).FirstOrDefault();
        }
        public async Task<T> FirstOrDefaultAsync()
        {
            return await _repository.Get(_filter, _orderByQuerable, _includeProperties, _includePropertiesStrings, _page, _pageSize).FirstOrDefaultAsync();
        }
        public async Task<T> FirstOrDefaultNoTrackingAsync()
        {
            return await _repository.Get(_filter, _orderByQuerable, _includeProperties, _includePropertiesStrings, _page, _pageSize).AsNoTracking().FirstOrDefaultAsync();
        }

        public IEnumerable<T> ToList()
        {
            return _repository.Get(_filter, _orderByQuerable, _includeProperties, _includePropertiesStrings, _page, _pageSize).ToList();
        }
        public async Task<List<T>> ToListAsync()
        {
            return await _repository.Get(_filter, _orderByQuerable, _includeProperties, _includePropertiesStrings, _page, _pageSize).ToListAsync();
        }
        public async Task<List<T>> ToListNoTrackingAsync()
        {
            return await _repository.Get(_filter, _orderByQuerable, _includeProperties, _includePropertiesStrings, _page, _pageSize).AsNoTracking().ToListAsync();
        }
    }
}
