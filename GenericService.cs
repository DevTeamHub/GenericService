using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DevTeam.EntityFrameworkExtensions.DbContext;
using DevTeam.GenericRepository;
using DevTeam.GenericService.Pagination;
using DevTeam.QueryMappings;

namespace DevTeam.GenericService
{
    public class GenericService: IGenericService
    {
        protected readonly IGenericRepository Repository; 

        public GenericService(IGenericRepository repository)
        {
            Repository = repository;
        }

        public virtual TModel Get<TEntity, TModel>(int id)
            where TEntity: class, IEntity
            where TModel : class
        {
            return Repository.GetQuery<TEntity>(id).AsModel<TModel>();
        }

        public virtual TModel Get<TEntity, TModel>(Expression<Func<TEntity, bool>> expression)
            where TEntity: class
            where TModel : class
        {
            return Repository.GetList(expression).AsModel<TModel>();
        }

        public virtual List<TModel> GetList<TEntity, TModel>(Expression<Func<TEntity, bool>> expression = null)
            where TEntity: class
            where TModel : class
        {
            return Repository.GetList(expression).AsModelList<TModel>().ToList();
        }

        public virtual PaginationModel<TModel> Search<TEntity, TModel>(PaginationParams<TEntity> pagination)
            where TModel : class, new()
            where TEntity : class, new()
        {
            var query = Repository.Query<TEntity>();
            var model = pagination.Find(query);

            var count = model.Count / pagination.Take;
            if (model.Count % pagination.Take != 0)
            {
                count += 1;
            }

            return new PaginationModel<TModel>
            {
                List = model.Query.AsModelList<TModel>().ToList(),
                Count = count
            };
        }

        public virtual int Create<TModel, TEntity>(TModel model)
            where TEntity : class, IEntity, new()
            where TModel : class
        {
            var entity = model.AsEntity<TModel, TEntity>();
            Repository.Add(entity);
            Repository.Save();
            return entity.PrimaryKey;
        }

        public virtual void CreateRange<TModel, TEntity>(List<TModel> models)
            where TEntity : class, new()
        {
            var entities = models.AsEntityList<TModel, TEntity>();
            Repository.AddRange(entities);
            Repository.Save();
        }

        public virtual void Update<TModel, TEntity>(int id, TModel model)
            where TEntity: class, IEntity, new()
            where TModel : class
        {
            var entity = Repository.Get<TEntity>(id);
            entity.Update(model);
            Repository.Save();
        }

        public virtual void Delete<TEntity>(int id)
            where TEntity: class, IEntity
        {
            Repository.Delete<TEntity>(id);
            Repository.Save();
        }

        public virtual void Delete<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class
        {
            Repository.Delete(predicate);
            Repository.Save();
        }
    }
    
}
