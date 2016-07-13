using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DevTeam.EntityFrameworkExtensions.DbContext;
using DevTeam.GenericService.Pagination;

namespace DevTeam.GenericService
{
    public class GenericService<TEntity> : IGenericService<TEntity>
        where TEntity: class, IEntity, new()
    {
        protected readonly IGenericService Service;

        public GenericService(IGenericService service)
        {
            Service = service;
        }

        public virtual TModel Get<TModel>(int id)
            where TModel : class
        {
            return Service.Get<TEntity, TModel>(id);
        }

        public virtual TModel Get<TModel>(Expression<Func<TEntity, bool>> expression)
            where TModel : class
        {
            return Service.Get<TEntity, TModel>(expression);
        }

        public virtual List<TModel> GetList<TModel>(Expression<Func<TEntity, bool>> expression = null)
            where TModel : class
        {
            return Service.GetList<TEntity, TModel>(expression);
        }

        public virtual PaginationModel<TModel> Search<TModel>(PaginationParams<TEntity> pagination)
            where TModel : class, new()
        {
            return Service.Search<TEntity, TModel>(pagination);
        }

        public virtual int Create<TModel>(TModel model)
            where TModel : class
        {
            return Service.Create<TModel, TEntity>(model);
        }

        public virtual void CreateRange<TModel>(List<TModel> models)
        {
            Service.CreateRange<TModel, TEntity>(models);
        }

        public virtual void Update<TModel>(int id, TModel model)
            where TModel : class
        {
            Service.Update<TModel, TEntity>(id, model);
        }

        public virtual void Delete(int id)
        {
            Service.Delete<TEntity>(id);
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            Service.Delete(predicate);
        }
    }
}
