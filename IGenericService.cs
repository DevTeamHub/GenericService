using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DevTeam.EntityFrameworkExtensions.DbContext;
using DevTeam.GenericService.Pagination;

namespace DevTeam.GenericService
{
    public interface IGenericService
    {
        TModel Get<TEntity, TModel>(int id)
            where TEntity: class, IEntity
            where TModel : class;

        TModel Get<TEntity, TModel>(Expression<Func<TEntity, bool>> expression)
            where TEntity : class
            where TModel : class;

        List<TModel> GetList<TEntity, TModel>(Expression<Func<TEntity, bool>> expression = null)
            where TEntity: class
            where TModel : class;

        PaginationModel<TModel> Search<TEntity, TModel>(PaginationParams<TEntity> pagination)
            where TModel : class, new()
            where TEntity : class, new();

        int Create<TModel, TEntity>(TModel model)
            where TEntity : class, IEntity, new()
            where TModel : class;

        void CreateRange<TModel, TEntity>(List<TModel> models)
            where TEntity : class, new();

        void Update<TModel, TEntity>(int id, TModel model)
            where TEntity : class, IEntity, new()
            where TModel : class;

        void Delete<TEntity>(int id)
            where TEntity : class, IEntity;

        void Delete<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class;
    }
    
}
