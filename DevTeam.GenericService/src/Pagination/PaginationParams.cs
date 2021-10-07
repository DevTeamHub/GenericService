using System;
using System.Linq;
using System.Linq.Expressions;

namespace DevTeam.GenericService.Pagination
{
    public abstract class PaginationParams<TEntity>
        where TEntity : class, new()
    {
        public virtual string Search { get; set; }

        public int Skip { get; set; }

        public virtual int Take { get; set; }

        protected abstract Expression<Func<TEntity, string>> SearchBy();

        protected abstract IQueryable<TEntity> Ordering(IQueryable<TEntity> query);


        //public virtual SearchResult<TEntity> Find(IQueryable<TEntity> query)
        //{
        //    query = Filter(query);

        //    if (!string.IsNullOrEmpty(Search))
        //    {
        //        query = SearchFilter(query);
        //    }

        //    return new SearchResult<TEntity>
        //    {
        //        Query = Ordering(query).Skip(Skip).Take(Take),
        //        Count = query.Count()
        //    };
        //}

        protected virtual IQueryable<TEntity> Filter(IQueryable<TEntity> query)
        {
            return query;
        }

        //protected virtual IQueryable<TEntity> SearchFilter(IQueryable<TEntity> query)
        //{
        //    var expression = ExpressionHelper.Search(SearchBy(), Search);
        //    return query.Where(expression);
        //}
    }
}
