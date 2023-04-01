using DevTeam.EntityFrameworkExtensions;
using DevTeam.GenericRepository;
using DevTeam.QueryMappings.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevTeam.GenericService
{
    public class SoftDeleteGenericService : SoftDeleteGenericService<IDbContext>, ISoftDeleteGenericService
    {
        public SoftDeleteGenericService(
            IMappingService<IDbContext> mappings,
            ISoftDeleteRepository<IDbContext> repository, 
            IReadOnlyDeleteRepository<IDbContext> readRepository) 
            : base(mappings, repository, readRepository)
        {
        }
    }

    public class SoftDeleteGenericService<TContext> : GenericService<TContext>, ISoftDeleteGenericService<TContext>
        where TContext : IDbContext
    {
        public SoftDeleteGenericService(
            IMappingService<TContext> mappings, 
            ISoftDeleteRepository<TContext> repository, 
            IReadOnlyDeleteRepository<TContext> readRepository) 
            : base(mappings, repository, readRepository)
        { }
    }

    public class GenericService : GenericService<IDbContext>, IGenericService
    {
        public GenericService(
            IMappingService<IDbContext> mappings,
            IRepository repository,
            IReadOnlyRepository readRepository)
            : base(mappings, repository, readRepository)
        { }
    }

    public class GenericService<TContext>: IGenericService<TContext>
        where TContext: IDbContext
    {
        private readonly IMappingService _mappings;
        private readonly IReadOnlyRepository<TContext> _readRepository;
        private readonly IRepository<TContext> _writeRepository; 

        public GenericService(
            IMappingService<TContext> mappings,
            IRepository<TContext> repository,
            IReadOnlyRepository<TContext> readRepository)
        {
            _mappings = mappings;
            _readRepository = readRepository;
            _writeRepository = repository;
        }

        #region Get List

        public virtual IQueryable<TModel> QueryList<TEntity, TModel>(Expression<Func<TEntity, bool>>? filter = null, string? mappingName = null)
            where TEntity : class
        {
            var query = _readRepository.GetList(filter);
            return _mappings.Map<TEntity, TModel>(query, mappingName);
        }

        public virtual IQueryable<TModel> QueryList<TEntity, TModel, TArgs>(Expression<Func<TEntity, bool>>? filter = null, TArgs? args = null, string? mappingName = null)
            where TEntity : class
            where TArgs: class
        {
            var query = _readRepository.GetList(filter);
            return _mappings.Map<TEntity, TModel, TArgs>(query, args, mappingName);
        }

        public virtual List<TModel> GetList<TEntity, TModel>(Expression<Func<TEntity, bool>>? filter = null, string? mappingName = null)
            where TEntity : class
        {
            return QueryList<TEntity, TModel>(filter, mappingName).ToList();
        }

        public virtual List<TModel> GetList<TEntity, TModel, TArgs>(Expression<Func<TEntity, bool>>? filter = null, TArgs? args = null, string? mappingName = null)
            where TEntity : class
            where TArgs: class
        {
            return QueryList<TEntity, TModel, TArgs>(filter, args, mappingName).ToList();
        }

        public virtual Task<List<TModel>> GetListAsync<TEntity, TModel>(Expression<Func<TEntity, bool>>? filter = null, string? mappingName = null)
            where TEntity : class
        {
            return QueryList<TEntity, TModel>(filter, mappingName).ToListAsync();
        }

        public virtual Task<List<TModel>> GetListAsync<TEntity, TModel, TArgs>(Expression<Func<TEntity, bool>>? filter = null, TArgs? args = null, string? mappingName = null)
            where TEntity : class
            where TArgs : class
        {
            return QueryList<TEntity, TModel, TArgs>(filter, args, mappingName).ToListAsync();
        }

        #endregion

        #region Get One

        public virtual IQueryable<TModel> QueryOne<TEntity, TModel, TKey>(TKey id, string? mappingName = null)
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey> 
        {
            var query = _readRepository.QueryOne<TEntity, TKey>(id);
            return _mappings.Map<TEntity, TModel>(query, mappingName);
        }

        public virtual IQueryable<TModel> QueryOne<TEntity, TModel, TArgs, TKey>(TKey id, TArgs args, string? mappingName = null)
            where TEntity : class, IEntity<TKey>
            where TArgs: class
            where TKey : IEquatable<TKey>
        {
            var query = _readRepository.QueryOne<TEntity, TKey>(id);
            return _mappings.Map<TEntity, TModel, TArgs>(query, args, mappingName);
        }

        public virtual IQueryable<TModel> QueryOne<TEntity, TModel>(int id, string? mappingName = null)
            where TEntity : class, IEntity
        {
            return QueryOne<TEntity, TModel, int>(id, mappingName);
        }

        public virtual IQueryable<TModel> QueryOne<TEntity, TModel, TArgs>(int id, TArgs args, string? mappingName = null)
            where TEntity : class, IEntity
            where TArgs: class
        {
            return QueryOne<TEntity, TModel, TArgs, int>(id, args, mappingName);
        }

        public virtual TModel Get<TEntity, TModel>(Expression<Func<TEntity, bool>> filter, string? mappingName = null)
            where TEntity : class
        {
            return QueryList<TEntity, TModel>(filter, mappingName).FirstOrDefault();
        }

        public virtual TModel Get<TEntity, TModel, TArgs>(Expression<Func<TEntity, bool>> filter, TArgs args, string? mappingName = null)
            where TEntity : class
            where TArgs : class
        {
            return QueryList<TEntity, TModel, TArgs>(filter, args, mappingName).FirstOrDefault();
        }

        public virtual Task<TModel> GetAsync<TEntity, TModel>(Expression<Func<TEntity, bool>> filter, string? mappingName = null)
            where TEntity : class
        {
            return QueryList<TEntity, TModel>(filter, mappingName).FirstOrDefaultAsync();
        }

        public virtual Task<TModel> GetAsync<TEntity, TModel, TArgs>(Expression<Func<TEntity, bool>> filter, TArgs args, string? mappingName = null)
            where TEntity : class
            where TArgs : class
        {
            return QueryList<TEntity, TModel, TArgs>(filter, args, mappingName).FirstOrDefaultAsync();
        }

        public virtual TModel Get<TEntity, TModel, TKey>(TKey id, string? mappingName = null)
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey>
        {
            return QueryOne<TEntity, TModel, TKey>(id, mappingName).FirstOrDefault();
        }

        public virtual TModel Get<TEntity, TModel, TArgs, TKey>(TKey id, TArgs args, string? mappingName = null)
            where TEntity : class, IEntity<TKey>
            where TArgs: class
            where TKey : IEquatable<TKey>
        {
            return QueryOne<TEntity, TModel, TArgs, TKey>(id, args, mappingName).FirstOrDefault();
        }

        public virtual Task<TModel> GetAsync<TEntity, TModel, TKey>(TKey id, string? mappingName = null)
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey>
        {
            return QueryOne<TEntity, TModel, TKey>(id, mappingName).FirstOrDefaultAsync();
        }

        public virtual Task<TModel> GetAsync<TEntity, TModel, TArgs, TKey>(TKey id, TArgs args, string? mappingName = null)
            where TEntity : class, IEntity<TKey>
            where TArgs: class
            where TKey : IEquatable<TKey>
        {
            return QueryOne<TEntity, TModel, TArgs, TKey>(id, args, mappingName).FirstOrDefaultAsync();
        }

        public virtual TModel Get<TEntity, TModel>(int id, string? mappingName = null)
            where TEntity : class, IEntity
        {
            return QueryOne<TEntity, TModel>(id, mappingName).FirstOrDefault();
        }

        public virtual TModel Get<TEntity, TModel, TArgs>(int id, TArgs args, string? mappingName = null)
            where TEntity : class, IEntity
            where TArgs: class
        {
            return QueryOne<TEntity, TModel, TArgs>(id, args, mappingName).FirstOrDefault();
        }

        public virtual Task<TModel> GetAsync<TEntity, TModel>(int id, string? mappingName = null)
            where TEntity : class, IEntity
        {
            return QueryOne<TEntity, TModel>(id, mappingName).FirstOrDefaultAsync();
        }

        public virtual Task<TModel> GetAsync<TEntity, TModel, TArgs>(int id, TArgs args, string? mappingName = null)
            where TEntity : class, IEntity
            where TArgs: class
        {
            return QueryOne<TEntity, TModel, TArgs>(id, args, mappingName).FirstOrDefaultAsync();
        }

        #endregion

        #region Get Property

        public virtual TProperty GetProperty<TEntity, TProperty>(Expression<Func<TEntity, bool>> filter,
                                                         Expression<Func<TEntity, TProperty>> selector)
            where TEntity : class
        {
            return _readRepository.GetProperty(filter, selector);
        }

        public virtual Task<TProperty> GetPropertyAsync<TEntity, TProperty>(Expression<Func<TEntity, bool>> filter,
                                                                    Expression<Func<TEntity, TProperty>> selector)
            where TEntity : class
        {
            return _readRepository.GetPropertyAsync(filter, selector);
        }

        public virtual TProperty GetProperty<TEntity, TProperty, TKey>(TKey id, Expression<Func<TEntity, TProperty>> selector)
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey>
        {
            return _readRepository.GetProperty(id, selector);
        }

        public virtual Task<TProperty> GetPropertyAsync<TEntity, TProperty, TKey>(TKey id, Expression<Func<TEntity, TProperty>> selector)
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey>
        {
            return _readRepository.GetPropertyAsync(id, selector);
        }

        public virtual TProperty GetProperty<TEntity, TProperty>(int id, Expression<Func<TEntity, TProperty>> selector)
            where TEntity : class, IEntity
        {
            return _readRepository.GetProperty(id, selector);
        }

        public virtual Task<TProperty> GetPropertyAsync<TEntity, TProperty>(int id, Expression<Func<TEntity, TProperty>> selector)
            where TEntity : class, IEntity
        {
            return _readRepository.GetPropertyAsync(id, selector);
        }

        #endregion

        #region Any

        public virtual bool Any<TEntity>(Expression<Func<TEntity, bool>>? filter = null)
            where TEntity : class
        {
            return _readRepository.Any(filter);
        }

        public virtual Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>>? filter = null)
            where TEntity : class
        {
            return _readRepository.AnyAsync(filter);
        }

        #endregion

        #region Add

        public virtual TEntity Add<TModel, TEntity>(TModel model, string? addMappingName = null)
            where TEntity : class
        {
            var entity = _mappings.Map<TModel, TEntity>(model, addMappingName);

            _writeRepository.Add(entity);
            _writeRepository.Save();

            return entity;
        }

        public virtual TEntity Add<TModel, TEntity, TArgs>(TModel model, TArgs addMappingArgs, string? addMappingName = null)
            where TEntity : class
            where TArgs : class
        {
            var entity = _mappings.Map<TModel, TEntity, TArgs>(model, addMappingArgs, addMappingName);

            _writeRepository.Add(entity);
            _writeRepository.Save();

            return entity;
        }

        public virtual TResult Add<TModel, TEntity, TResult, TKey>(TModel model, string? addMappingName = null, string? getMappingName = null)
            where TEntity : class, IEntity<TKey>
            where TKey: IEquatable<TKey>
        {
            var entity = Add<TModel, TEntity>(model, addMappingName);
            return Get<TEntity, TResult, TKey>(entity.Id, getMappingName);
        }

        public virtual TResult Add<TModel, TEntity, TResult>(TModel model, string? addMappingName = null, string? getMappingName = null)
            where TEntity : class, IEntity
        {
            return Add<TModel, TEntity, TResult, int>(model, addMappingName, getMappingName);
        }

        public virtual async Task<TEntity> AddAsync<TModel, TEntity>(TModel model, string? addMappingName = null)
            where TEntity : class
        {
            var entity = _mappings.Map<TModel, TEntity>(model, addMappingName);

            await _writeRepository.AddAsync(entity);
            await _writeRepository.SaveAsync();

            return entity;
        }

        public virtual async Task<TEntity> AddAsync<TModel, TEntity, TArgs>(TModel model, TArgs args, string? addMappingName = null)
            where TEntity : class
            where TArgs : class
        {
            var entity = _mappings.Map<TModel, TEntity, TArgs>(model, args, addMappingName);

            await _writeRepository.AddAsync(entity);
            await _writeRepository.SaveAsync();

            return entity;
        }

        public virtual async Task<TResult> AddAsync<TModel, TEntity, TResult, TKey>(TModel model, string? addMappingName = null, string? getMappingName = null)
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey>
        {
            var entity = await AddAsync<TModel, TEntity>(model, addMappingName);
            return await GetAsync<TEntity, TResult, TKey>(entity.Id, getMappingName);
        }

        public virtual Task<TResult> AddAsync<TModel, TEntity, TResult>(TModel model, string? addMappingName = null, string? getMappingName = null)
            where TEntity : class, IEntity
        {
            return AddAsync<TModel, TEntity, TResult, int>(model, addMappingName, getMappingName);
        }

        #endregion

        #region AddRange

        public virtual List<TEntity> AddRange<TModel, TEntity>(List<TModel> models, string? addMappingName = null)
            where TEntity : class
        {
            var entities = _mappings.Map<TModel, TEntity>(models, addMappingName);

            _writeRepository.AddRange(entities);
            _writeRepository.Save();

            return entities;
        }

        public virtual List<TEntity> AddRange<TModel, TEntity, TArgs>(List<TModel> models, TArgs args, string? addMappingName = null)
            where TEntity : class
            where TArgs : class
        {
            var entities = _mappings.Map<TModel, TEntity, TArgs>(models, args, addMappingName);

            _writeRepository.AddRange(entities);
            _writeRepository.Save();

            return entities;
        }

        public virtual List<TResult> AddRange<TModel, TEntity, TResult, TKey>(List<TModel> models, string? addMappingName = null, string? getMappingName = null)
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey>
        {
            var entities = AddRange<TModel, TEntity>(models, addMappingName);

            var ids = entities.Select(x => x.Id).ToList();
            var results = GetList<TEntity, TResult>(x => ids.Contains(x.Id), getMappingName);

            return results;
        }

        public virtual List<TResult> AddRange<TModel, TEntity, TResult>(List<TModel> models, string? addMappingName = null, string? getMappingName = null)
            where TEntity : class, IEntity
        {
            return AddRange<TModel, TEntity, TResult, int>(models, addMappingName, getMappingName);
        }

        public virtual async Task<List<TEntity>> AddRangeAsync<TModel, TEntity>(List<TModel> models, string? addMappingName = null)
            where TEntity : class
        {
            var entities = _mappings.Map<TModel, TEntity>(models, addMappingName);

            await _writeRepository.AddRangeAsync(entities);
            await _writeRepository.SaveAsync();

            return entities;
        }

        public virtual async Task<List<TEntity>> AddRangeAsync<TModel, TEntity, TArgs>(List<TModel> models, TArgs args, string? addMappingName = null)
            where TEntity : class
            where TArgs : class
        {
            var entities = _mappings.Map<TModel, TEntity, TArgs>(models, args, addMappingName);

            await _writeRepository.AddRangeAsync(entities);
            await _writeRepository.SaveAsync();

            return entities;
        }

        public virtual async Task<List<TResult>> AddRangeAsync<TModel, TEntity, TResult, TKey>(List<TModel> models, string? addMappingName = null, string? getMappingName = null)
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey>
        {
            var entities = await AddRangeAsync<TModel, TEntity>(models, addMappingName);

            var ids = entities.Select(x => x.Id).ToList();
            var results = await GetListAsync<TEntity, TResult>(x => ids.Contains(x.Id), getMappingName);

            return results;
        }

        public virtual Task<List<TResult>> AddRangeAsync<TModel, TEntity, TResult>(List<TModel> models, string? addMappingName = null, string? getMappingName = null)
            where TEntity : class, IEntity
        {
            return AddRangeAsync<TModel, TEntity, TResult, int>(models, addMappingName, getMappingName);
        }

        #endregion

        #region Update

        public virtual TEntity Update<TModel, TEntity, TKey>(TKey id, TModel model, Action<TModel, TEntity> updateFunc)
            where TEntity : class, IEntity<TKey>
            where TKey: IEquatable<TKey>
        {
            var entity = _writeRepository.Get<TEntity, TKey>(id);
            updateFunc(model, entity);
            _writeRepository.Save();

            return entity;
        }

        public virtual TResult Update<TModel, TEntity, TResult, TKey>(TKey id, TModel model, Action<TModel, TEntity> updateFunc)
            where TEntity : class, IEntity<TKey>
            where TKey: IEquatable<TKey>
        {
            Update(id, model, updateFunc);
            return Get<TEntity, TResult, TKey>(id);
        }

        public virtual TResult Update<TModel, TEntity, TResult>(int id, TModel model, Action<TModel, TEntity> updateFunc)
            where TEntity : class, IEntity
        {
            Update(id, model, updateFunc);
            return Get<TEntity, TResult>(id);
        }

        public virtual async Task<TEntity> UpdateAsync<TModel, TEntity, TKey>(TKey id, TModel model, Action<TModel, TEntity> updateFunc)
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey>
        {
            var entity = await _writeRepository.GetAsync<TEntity, TKey>(id);
            updateFunc(model, entity);
            await _writeRepository.SaveAsync();

            return entity;
        }

        public virtual async Task<TResult> UpdateAsync<TModel, TEntity, TResult, TKey>(TKey id, TModel model, Action<TModel, TEntity> updateFunc)
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey>
        {
            await UpdateAsync(id, model, updateFunc);
            return await GetAsync<TEntity, TResult, TKey>(id);
        }

        public virtual async Task<TResult> UpdateAsync<TModel, TEntity, TResult>(int id, TModel model, Action<TModel, TEntity> updateFunc)
            where TEntity : class, IEntity
        {
            await UpdateAsync(id, model, updateFunc);
            return await GetAsync<TEntity, TResult>(id);
        }

        #endregion

        #region Delete

        public virtual int Delete<TEntity>(int id)
            where TEntity : class, IEntity
        {
            _writeRepository.Delete<TEntity>(id);
            return _writeRepository.Save();
        } 

        public virtual int Delete<TEntity>(Expression<Func<TEntity, bool>> filter)
            where TEntity : class
        {
            _writeRepository.Delete(filter);
            return _writeRepository.Save();
        }
        
        public virtual async Task<int> DeleteAsync<TEntity>(int id)
            where TEntity : class, IEntity
        {
            await _writeRepository.DeleteAsync<TEntity>(id);
            return await _writeRepository.SaveAsync();
        }

        public virtual async Task<int> DeleteAsync<TEntity>(Expression<Func<TEntity, bool>> filter)
            where TEntity : class
        {
            await _writeRepository.DeleteAsync(filter);
            return await _writeRepository.SaveAsync();
        }

        #endregion

        #region Delete Range

        public virtual int DeleteRange<TEntity>(Expression<Func<TEntity, bool>> filter)
            where TEntity : class
        {
            _writeRepository.DeleteRange(filter);
            return _writeRepository.Save();
        }

        public virtual async Task<int> DeleteRangeAsync<TEntity>(Expression<Func<TEntity, bool>> filter)
            where TEntity : class
        {
            await _writeRepository.DeleteRangeAsync(filter);
            return await _writeRepository.SaveAsync();
        }

        #endregion
    }
}
