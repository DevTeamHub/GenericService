using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DevTeam.EntityFrameworkExtensions.DbContext;
using DevTeam.GenericService.Pagination;

namespace DevTeam.GenericService
{
    public interface IGenericService<TEntity>
        where TEntity: class, IEntity, new()
    {
        TModel Get<TModel>(int id)
            where TModel : class;

        TModel Get<TModel>(Expression<Func<TEntity, bool>> expression)
            where TModel : class;

        List<TModel> GetList<TModel>(Expression<Func<TEntity, bool>> expression = null)
            where TModel : class;

        PaginationModel<TModel> Search<TModel>(PaginationParams<TEntity> pagination)
            where TModel : class, new();

        int Create<TModel>(TModel model)
            where TModel : class;

        void CreateRange<TModel>(List<TModel> models);

        void Update<TModel>(int id, TModel model)
            where TModel : class;

        void Delete(int id);

        void Delete(Expression<Func<TEntity, bool>> predicate);
    }
}
