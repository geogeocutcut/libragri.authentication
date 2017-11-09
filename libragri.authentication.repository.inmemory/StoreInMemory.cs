using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using libragri.core.common;
using libragri.core.repository;

namespace libragri.authentication.repository.inmemory
{
    public class StoreInMemory<TId> : IStore<TId>
    {
        private IDictionary<Type,IDictionary<TId,object>> data = new Dictionary<Type,IDictionary<TId,object>>();

        public IList<TEntity> FindAll<TEntity>() where TEntity:Entity<TId>
        {
            data.TryGetValue(typeof(TEntity),out var dataEntity);
            if(dataEntity==null)
            {
                return null;
            }
            return ((ICollection<TEntity>)dataEntity.Values).ToList();
        }

        public TEntity FindById<TEntity>(TId id) where TEntity:Entity<TId>
        { 
            data.TryGetValue(typeof(TEntity),out var dataEntity);
            if(dataEntity==null)
            {
                return default(TEntity);
            }
            dataEntity.TryGetValue(id,out object entity);
            return (TEntity)entity;
        }

        public IList<TEntity> FindWhere<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity:Entity<TId>
        {
            data.TryGetValue(typeof(TEntity),out var dataEntity);
            return dataEntity?.Select(x=>(TEntity)x.Value)?.AsQueryable().Where(predicate)?.ToList();
        }

        public void Remove<TEntity>(TEntity entity) where TEntity:Entity<TId>
        {
            data.TryGetValue(typeof(TEntity),out var dataEntity);
            dataEntity?.Remove(entity.Id);
        }

        public void Upsert<TEntity>(TEntity entity) where TEntity:Entity<TId>
        {
            data.TryGetValue(typeof(TEntity),out var dataEntity);
            if(dataEntity==null)
            {
                data[typeof(TEntity)]= new Dictionary<TId,object>();
            }
            data[typeof(TEntity)][entity.Id]=entity;
        }
    }
}