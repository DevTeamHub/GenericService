using DevTeam.EntityFrameworkExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevTeam.GenericService
{
    public interface IGenericService
    {
        IQueryable<TModel> QueryList<TEntity, TModel>(Expression<Func<TEntity, bool>>? filter = null)
            where TEntity : class;
        List<TModel> GetList<TEntity, TModel>(Expression<Func<TEntity, bool>>? filter = null)
            where TEntity : class;
        Task<List<TModel>> GetListAsync<TEntity, TModel>(Expression<Func<TEntity, bool>>? filter = null)
            where TEntity : class;
        IQueryable<TModel> QueryOne<TEntity, TModel, TKey>(TKey id)
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey>;
        IQueryable<TModel> QueryOne<TEntity, TModel>(int id)
            where TEntity : class, IEntity;
        TModel Get<TEntity, TModel>(Expression<Func<TEntity, bool>> filter)
            where TEntity : class;
        Task<TModel> GetAsync<TEntity, TModel>(Expression<Func<TEntity, bool>> filter)
            where TEntity : class;
        TModel Get<TEntity, TModel, TKey>(TKey id)
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey>;
        Task<TModel> GetAsync<TEntity, TModel, TKey>(TKey id)
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey>;
        TModel Get<TEntity, TModel>(int id)
            where TEntity : class, IEntity;
        Task<TModel> GetAsync<TEntity, TModel>(int id)
            where TEntity : class, IEntity;
        TProperty GetProperty<TEntity, TProperty>(Expression<Func<TEntity, bool>> filter,
                                                  Expression<Func<TEntity, TProperty>> selector)
            where TEntity : class;
        Task<TProperty> GetPropertyAsync<TEntity, TProperty>(Expression<Func<TEntity, bool>> filter,
                                                             Expression<Func<TEntity, TProperty>> selector)
            where TEntity : class;
        TProperty GetProperty<TEntity, TProperty, TKey>(TKey id, Expression<Func<TEntity, TProperty>> selector)
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey>;
        Task<TProperty> GetPropertyAsync<TEntity, TProperty, TKey>(TKey id, Expression<Func<TEntity, TProperty>> selector)
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey>;
        TProperty GetProperty<TEntity, TProperty>(int id, Expression<Func<TEntity, TProperty>> selector)
            where TEntity : class, IEntity;
        Task<TProperty> GetPropertyAsync<TEntity, TProperty>(int id, Expression<Func<TEntity, TProperty>> selector)
            where TEntity : class, IEntity;
        bool Any<TEntity>(Expression<Func<TEntity, bool>>? filter = null)
            where TEntity : class;
        Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>>? filter = null)
            where TEntity : class;
        TEntity Add<TModel, TEntity>(TModel model)
            where TEntity : class;
        TResult Add<TModel, TEntity, TResult, TKey>(TModel model)
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey>;
        TResult Add<TModel, TEntity, TResult>(TModel model)
            where TEntity : class, IEntity;
        Task<TEntity> AddAsync<TModel, TEntity>(TModel model)
            where TEntity : class;
        Task<TResult> AddAsync<TModel, TEntity, TResult, TKey>(TModel model)
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey>;
        Task<TResult> AddAsync<TModel, TEntity, TResult>(TModel model)
            where TEntity : class, IEntity;
        List<TEntity> AddRange<TModel, TEntity>(List<TModel> models)
            where TEntity : class;
        List<TResult> AddRange<TModel, TEntity, TResult, TKey>(List<TModel> models)
            where TEntity : class, IEntity<TKey>
            where TKey: IEquatable<TKey>;
        List<TResult> AddRange<TModel, TEntity, TResult>(List<TModel> models)
            where TEntity : class, IEntity;
        Task<List<TEntity>> AddRangeAsync<TModel, TEntity>(List<TModel> models)
            where TEntity : class;
        Task<List<TResult>> AddRangeAsync<TModel, TEntity, TResult, TKey>(List<TModel> models)
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey>;
        Task<List<TResult>> AddRangeAsync<TModel, TEntity, TResult>(List<TModel> models)
            where TEntity : class, IEntity;
        TEntity Update<TModel, TEntity, TKey>(TKey id, TModel model, Action<TModel, TEntity> updateFunc)
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey>;
        TResult Update<TModel, TEntity, TResult, TKey>(TKey id, TModel model, Action<TModel, TEntity> updateFunc)
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey>;
        TResult Update<TModel, TEntity, TResult>(int id, TModel model, Action<TModel, TEntity> updateFunc)
            where TEntity : class, IEntity;
        Task<TEntity> UpdateAsync<TModel, TEntity, TKey>(TKey id, TModel model, Action<TModel, TEntity> updateFunc)
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey>;
        Task<TResult> UpdateAsync<TModel, TEntity, TResult, TKey>(TKey id, TModel model, Action<TModel, TEntity> updateFunc)
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey>;
        Task<TResult> UpdateAsync<TModel, TEntity, TResult>(int id, TModel model, Action<TModel, TEntity> updateFunc)
            where TEntity : class, IEntity;
        void Delete<TEntity>(int id)
            where TEntity : class, IEntity;
        void Delete<TEntity>(Expression<Func<TEntity, bool>> filter)
            where TEntity : class;
        Task DeleteAsync<TEntity>(int id)
            where TEntity : class, IEntity;
        Task DeleteAsync<TEntity>(Expression<Func<TEntity, bool>> filter)
            where TEntity : class;
    }
}
