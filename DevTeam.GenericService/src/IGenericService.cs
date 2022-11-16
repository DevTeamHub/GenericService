using DevTeam.EntityFrameworkExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevTeam.GenericService
{
    public interface ISoftDeleteGenericService : ISoftDeleteGenericService<IDbContext>
    { }

    public interface ISoftDeleteGenericService<TContext> : IGenericService<TContext>
        where TContext : IDbContext
    { }

    public interface IGenericService: IGenericService<IDbContext>
    { }

    public interface IGenericService<TContext>
        where TContext: IDbContext
    {
        IQueryable<TModel> QueryList<TEntity, TModel>(Expression<Func<TEntity, bool>>? filter = null, string? mappingName = null)
            where TEntity : class;
        IQueryable<TModel> QueryList<TEntity, TModel, TArgs>(Expression<Func<TEntity, bool>>? filter = null, TArgs? args = null, string? mappingName = null)
            where TEntity : class
            where TArgs : class;
        List<TModel> GetList<TEntity, TModel>(Expression<Func<TEntity, bool>>? filter = null, string? mappingName = null)
            where TEntity : class;
        List<TModel> GetList<TEntity, TModel, TArgs>(Expression<Func<TEntity, bool>>? filter = null, TArgs? args = null, string? mappingName = null)
            where TEntity : class
            where TArgs : class;
        Task<List<TModel>> GetListAsync<TEntity, TModel>(Expression<Func<TEntity, bool>>? filter = null, string? mappingName = null)
            where TEntity : class;
        Task<List<TModel>> GetListAsync<TEntity, TModel, TArgs>(Expression<Func<TEntity, bool>>? filter = null, TArgs? args = null, string? mappingName = null)
            where TEntity : class
            where TArgs : class;
        IQueryable<TModel> QueryOne<TEntity, TModel, TKey>(TKey id, string? mappingName = null)
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey>;
        IQueryable<TModel> QueryOne<TEntity, TModel, TArgs, TKey>(TKey id, TArgs args, string? mappingName = null)
            where TEntity : class, IEntity<TKey>
            where TArgs : class
            where TKey : IEquatable<TKey>;
        IQueryable<TModel> QueryOne<TEntity, TModel>(int id, string? mappingName = null)
            where TEntity : class, IEntity;
        IQueryable<TModel> QueryOne<TEntity, TModel, TArgs>(int id, TArgs args, string? mappingName = null)
            where TEntity : class, IEntity
            where TArgs : class;
        TModel Get<TEntity, TModel>(Expression<Func<TEntity, bool>> filter, string? mappingName = null)
            where TEntity : class;
        TModel Get<TEntity, TModel, TArgs>(Expression<Func<TEntity, bool>> filter, TArgs args, string? mappingName = null)
            where TEntity : class
            where TArgs : class;
        Task<TModel> GetAsync<TEntity, TModel>(Expression<Func<TEntity, bool>> filter, string? mappingName = null)
            where TEntity : class;
        Task<TModel> GetAsync<TEntity, TModel, TArgs>(Expression<Func<TEntity, bool>> filter, TArgs args, string? mappingName = null)
            where TEntity : class
            where TArgs : class;
        TModel Get<TEntity, TModel, TKey>(TKey id, string? mappingName = null)
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey>;
        TModel Get<TEntity, TModel, TArgs, TKey>(TKey id, TArgs args, string? mappingName = null)
            where TEntity : class, IEntity<TKey>
            where TArgs: class
            where TKey : IEquatable<TKey>;
        Task<TModel> GetAsync<TEntity, TModel, TKey>(TKey id, string? mappingName = null)
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey>;
        TModel Get<TEntity, TModel>(int id, string? mappingName = null)
            where TEntity : class, IEntity;
        TModel Get<TEntity, TModel, TArgs>(int id, TArgs args, string? mappingName = null)
            where TEntity : class, IEntity
            where TArgs : class;
        Task<TModel> GetAsync<TEntity, TModel>(int id, string? mappingName = null)
            where TEntity : class, IEntity;
        Task<TModel> GetAsync<TEntity, TModel, TArgs>(int id, TArgs args, string? mappingName = null)
            where TEntity : class, IEntity
            where TArgs : class;
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
        TEntity Add<TModel, TEntity>(TModel model, string? addMappingName = null)
            where TEntity : class;
        TEntity Add<TModel, TEntity, TArgs>(TModel model, TArgs args, string? addMappingName = null)
            where TEntity : class
            where TArgs : class;
        TResult Add<TModel, TEntity, TResult, TKey>(TModel model, string? addMappingName = null, string? getMappingName = null)
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey>;
        TResult Add<TModel, TEntity, TResult>(TModel model, string? addMappingName = null, string? getMappingName = null)
            where TEntity : class, IEntity;
        Task<TEntity> AddAsync<TModel, TEntity>(TModel model, string? addMappingName = null)
            where TEntity : class;
        Task<TEntity> AddAsync<TModel, TEntity, TArgs>(TModel model, TArgs args, string? addMappingName = null)
            where TEntity : class
            where TArgs : class;
        Task<TResult> AddAsync<TModel, TEntity, TResult, TKey>(TModel model, string? addMappingName = null, string? getMappingName = null)
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey>;
        Task<TResult> AddAsync<TModel, TEntity, TResult>(TModel model, string? addMappingName = null, string? getMappingName = null)
            where TEntity : class, IEntity;
        List<TEntity> AddRange<TModel, TEntity>(List<TModel> models, string? addMappingName = null)
            where TEntity : class;
        List<TEntity> AddRange<TModel, TEntity, TArgs>(List<TModel> models, TArgs args, string? addMappingName = null)
            where TEntity : class
            where TArgs : class;
        List<TResult> AddRange<TModel, TEntity, TResult, TKey>(List<TModel> models, string? addMappingName = null, string? getMappingName = null)
            where TEntity : class, IEntity<TKey>
            where TKey: IEquatable<TKey>;
        List<TResult> AddRange<TModel, TEntity, TResult>(List<TModel> models, string? addMappingName = null, string? getMappingName = null)
            where TEntity : class, IEntity;
        Task<List<TEntity>> AddRangeAsync<TModel, TEntity>(List<TModel> models, string? addMappingName = null)
            where TEntity : class;
        Task<List<TEntity>> AddRangeAsync<TModel, TEntity, TArgs>(List<TModel> models, TArgs args, string? addMappingName = null)
            where TEntity : class
            where TArgs : class;
        Task<List<TResult>> AddRangeAsync<TModel, TEntity, TResult, TKey>(List<TModel> models, string? addMappingName = null, string? getMappingName = null)
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey>;
        Task<List<TResult>> AddRangeAsync<TModel, TEntity, TResult>(List<TModel> models, string? addMappingName = null, string? getMappingName = null)
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
        int Delete<TEntity>(int id)
            where TEntity : class, IEntity;
        int Delete<TEntity>(Expression<Func<TEntity, bool>> filter)
            where TEntity : class;
        Task<int> DeleteAsync<TEntity>(int id)
            where TEntity : class, IEntity;
        Task<int> DeleteAsync<TEntity>(Expression<Func<TEntity, bool>> filter)
            where TEntity : class;
        int DeleteRange<TEntity>(Expression<Func<TEntity, bool>> filter)
            where TEntity : class;
        Task<int> DeleteRangeAsync<TEntity>(Expression<Func<TEntity, bool>> filter)
             where TEntity : class;
    }
}
