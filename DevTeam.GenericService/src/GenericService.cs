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
    public class GenericService: GenericService<IDbContext>, IGenericService 
    {
        public GenericService(
            IMappingService mappings,
            IRepository repository,
            IReadOnlyRepository readRepository)
            :base(mappings, repository, readRepository)
        { }
    }

    public class GenericService<TContext>: IGenericService<TContext>
        where TContext: IDbContext
    {
        private readonly IMappingService _mappings;
        private readonly IReadOnlyRepository<TContext> _readRepository;
        private readonly IRepository<TContext> _writeRepository; 

        public GenericService(
            IMappingService mappings,
            IRepository<TContext> repository,
            IReadOnlyRepository<TContext> readRepository)
        {
            _mappings = mappings;
            _readRepository = readRepository;
            _writeRepository = repository;
        }

        #region Get List

        public IQueryable<TModel> QueryList<TEntity, TModel>(Expression<Func<TEntity, bool>>? filter = null)
            where TEntity : class
        {
            var query = _readRepository.GetList(filter);
            return _mappings.Map<TEntity, TModel>(query);
        }

        public List<TModel> GetList<TEntity, TModel>(Expression<Func<TEntity, bool>>? filter = null)
            where TEntity : class
        {
            return QueryList<TEntity, TModel>(filter).ToList();
        }

        public Task<List<TModel>> GetListAsync<TEntity, TModel>(Expression<Func<TEntity, bool>>? filter = null)
            where TEntity : class
        {
            return QueryList<TEntity, TModel>(filter).ToListAsync();
        }

        #endregion

        #region Get One

        public IQueryable<TModel> QueryOne<TEntity, TModel, TKey>(TKey id)
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey> 
        {
            var query = _readRepository.QueryOne<TEntity, TKey>(id);
            return _mappings.Map<TEntity, TModel>(query);
        }

        public IQueryable<TModel> QueryOne<TEntity, TModel>(int id)
            where TEntity : class, IEntity
        {
            return QueryOne<TEntity, TModel, int>(id);
        }

        public TModel Get<TEntity, TModel>(Expression<Func<TEntity, bool>> filter)
            where TEntity : class
        {
            return QueryList<TEntity, TModel>(filter).FirstOrDefault();
        }

        public Task<TModel> GetAsync<TEntity, TModel>(Expression<Func<TEntity, bool>> filter)
            where TEntity : class
        {
            return QueryList<TEntity, TModel>(filter).FirstOrDefaultAsync();
        }

        public TModel Get<TEntity, TModel, TKey>(TKey id)
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey>
        {
            return QueryOne<TEntity, TModel, TKey>(id).FirstOrDefault();
        }

        public Task<TModel> GetAsync<TEntity, TModel, TKey>(TKey id)
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey>
        {
            return QueryOne<TEntity, TModel, TKey>(id).FirstOrDefaultAsync();
        }

        public TModel Get<TEntity, TModel>(int id)
            where TEntity : class, IEntity
        {
            return QueryOne<TEntity, TModel>(id).FirstOrDefault();
        }

        public Task<TModel> GetAsync<TEntity, TModel>(int id)
            where TEntity : class, IEntity
        {
            return QueryOne<TEntity, TModel>(id).FirstOrDefaultAsync();
        }

        #endregion

        #region Get Property

        public TProperty GetProperty<TEntity, TProperty>(Expression<Func<TEntity, bool>> filter,
                                                         Expression<Func<TEntity, TProperty>> selector)
            where TEntity : class
        {
            return _readRepository.GetProperty(filter, selector);
        }

        public Task<TProperty> GetPropertyAsync<TEntity, TProperty>(Expression<Func<TEntity, bool>> filter,
                                                                    Expression<Func<TEntity, TProperty>> selector)
            where TEntity : class
        {
            return _readRepository.GetPropertyAsync(filter, selector);
        }

        public TProperty GetProperty<TEntity, TProperty, TKey>(TKey id, Expression<Func<TEntity, TProperty>> selector)
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey>
        {
            return _readRepository.GetProperty(id, selector);
        }

        public Task<TProperty> GetPropertyAsync<TEntity, TProperty, TKey>(TKey id, Expression<Func<TEntity, TProperty>> selector)
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey>
        {
            return _readRepository.GetPropertyAsync(id, selector);
        }

        public TProperty GetProperty<TEntity, TProperty>(int id, Expression<Func<TEntity, TProperty>> selector)
            where TEntity : class, IEntity
        {
            return _readRepository.GetProperty(id, selector);
        }

        public Task<TProperty> GetPropertyAsync<TEntity, TProperty>(int id, Expression<Func<TEntity, TProperty>> selector)
            where TEntity : class, IEntity
        {
            return _readRepository.GetPropertyAsync(id, selector);
        }

        #endregion

        #region Any

        public bool Any<TEntity>(Expression<Func<TEntity, bool>>? filter = null)
            where TEntity : class
        {
            return _readRepository.Any(filter);
        }

        public Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>>? filter = null)
            where TEntity : class
        {
            return _readRepository.AnyAsync(filter);
        }

        #endregion

        #region Add

        public TEntity Add<TModel, TEntity>(TModel model)
            where TEntity : class
        {
            var entity = _mappings.Map<TModel, TEntity>(model);

            _writeRepository.Add(entity);
            _writeRepository.Save();

            return entity;
        }

        public TResult Add<TModel, TEntity, TResult, TKey>(TModel model)
            where TEntity : class, IEntity<TKey>
            where TKey: IEquatable<TKey>
        {
            var entity = Add<TModel, TEntity>(model);
            return Get<TEntity, TResult, TKey>(entity.Id);
        }

        public TResult Add<TModel, TEntity, TResult>(TModel model)
            where TEntity : class, IEntity
        {
            return Add<TModel, TEntity, TResult, int>(model);
        }

        public async Task<TEntity> AddAsync<TModel, TEntity>(TModel model)
            where TEntity : class
        {
            var entity = _mappings.Map<TModel, TEntity>(model);

            await _writeRepository.AddAsync(entity);
            await _writeRepository.SaveAsync();

            return entity;
        }

        public async Task<TResult> AddAsync<TModel, TEntity, TResult, TKey>(TModel model)
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey>
        {
            var entity = await AddAsync<TModel, TEntity>(model);
            return await GetAsync<TEntity, TResult, TKey>(entity.Id);
        }

        public Task<TResult> AddAsync<TModel, TEntity, TResult>(TModel model)
            where TEntity : class, IEntity
        {
            return AddAsync<TModel, TEntity, TResult, int>(model);
        }

        #endregion

        #region AddRange

        public List<TEntity> AddRange<TModel, TEntity>(List<TModel> models)
            where TEntity : class
        {
            var entities = _mappings.Map<TModel, TEntity>(models);

            _writeRepository.AddRange(entities);
            _writeRepository.Save();

            return entities;
        }

        public List<TResult> AddRange<TModel, TEntity, TResult, TKey>(List<TModel> models)
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey>
        {
            var entities = AddRange<TModel, TEntity>(models);

            var ids = entities.Select(x => x.Id).ToList();
            var results = GetList<TEntity, TResult>(x => ids.Contains(x.Id));

            return results;
        }

        public List<TResult> AddRange<TModel, TEntity, TResult>(List<TModel> models)
            where TEntity : class, IEntity
        {
            return AddRange<TModel, TEntity, TResult, int>(models);
        }

        public async Task<List<TEntity>> AddRangeAsync<TModel, TEntity>(List<TModel> models)
            where TEntity : class
        {
            var entities = _mappings.Map<TModel, TEntity>(models);

            await _writeRepository.AddRangeAsync(entities);
            await _writeRepository.SaveAsync();

            return entities;
        }

        public async Task<List<TResult>> AddRangeAsync<TModel, TEntity, TResult, TKey>(List<TModel> models)
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey>
        {
            var entities = await AddRangeAsync<TModel, TEntity>(models);

            var ids = entities.Select(x => x.Id).ToList();
            var results = await GetListAsync<TEntity, TResult>(x => ids.Contains(x.Id));

            return results;
        }

        public Task<List<TResult>> AddRangeAsync<TModel, TEntity, TResult>(List<TModel> models)
            where TEntity : class, IEntity
        {
            return AddRangeAsync<TModel, TEntity, TResult, int>(models);
        }

        #endregion

        #region Update

        public TEntity Update<TModel, TEntity, TKey>(TKey id, TModel model, Action<TModel, TEntity> updateFunc)
            where TEntity : class, IEntity<TKey>
            where TKey: IEquatable<TKey>
        {
            var entity = _writeRepository.Get<TEntity, TKey>(id);
            updateFunc(model, entity);
            _writeRepository.Save();

            return entity;
        }

        public TResult Update<TModel, TEntity, TResult, TKey>(TKey id, TModel model, Action<TModel, TEntity> updateFunc)
            where TEntity : class, IEntity<TKey>
            where TKey: IEquatable<TKey>
        {
            Update(id, model, updateFunc);
            return Get<TEntity, TResult, TKey>(id);
        }

        public TResult Update<TModel, TEntity, TResult>(int id, TModel model, Action<TModel, TEntity> updateFunc)
            where TEntity : class, IEntity
        {
            Update(id, model, updateFunc);
            return Get<TEntity, TResult>(id);
        }

        public async Task<TEntity> UpdateAsync<TModel, TEntity, TKey>(TKey id, TModel model, Action<TModel, TEntity> updateFunc)
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey>
        {
            var entity = await _writeRepository.GetAsync<TEntity, TKey>(id);
            updateFunc(model, entity);
            await _writeRepository.SaveAsync();

            return entity;
        }

        public async Task<TResult> UpdateAsync<TModel, TEntity, TResult, TKey>(TKey id, TModel model, Action<TModel, TEntity> updateFunc)
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey>
        {
            await UpdateAsync(id, model, updateFunc);
            return await GetAsync<TEntity, TResult, TKey>(id);
        }

        public async Task<TResult> UpdateAsync<TModel, TEntity, TResult>(int id, TModel model, Action<TModel, TEntity> updateFunc)
            where TEntity : class, IEntity
        {
            await UpdateAsync(id, model, updateFunc);
            return await GetAsync<TEntity, TResult>(id);
        }

        #endregion

        #region Delete

        public void Delete<TEntity>(int id)
            where TEntity : class, IEntity
        {
            _writeRepository.Delete<TEntity>(id);
        } 

        public void Delete<TEntity>(Expression<Func<TEntity, bool>> filter)
            where TEntity : class
        {
            _writeRepository.Delete(filter);
        }
        
        public Task DeleteAsync<TEntity>(int id)
            where TEntity : class, IEntity
        {
            return _writeRepository.DeleteAsync<TEntity>(id);
        }

        public Task DeleteAsync<TEntity>(Expression<Func<TEntity, bool>> filter)
            where TEntity : class
        {
            return _writeRepository.DeleteAsync(filter);
        }

        #endregion
    }
}
