
using libragri.core.cqrs;
using System;
using System.Collections.Generic;
using libragri.core.common;

namespace libragri.core.repository
{
    public class Repository<TId, TEntity> : IRepository<TId, TEntity> where TEntity : IAggregateRoot<TId>
    {
        IStore<TId, TEntity> store;

        public Repository(IStore<TId, TEntity> store)
        {
            this.store=store;
        }

        public void Delete(TEntity entity)
        {
            store.Remove(entity);
        }

        public IList<TEntity> GetAll()
        {
            return store.FindAll();
        }

        public TEntity GetById(TId id)
        {
            return store.FindById(id);
        }

        
        public IList<TEntity> FindWhere(System.Linq.Expressions.Expression<System.Func<TEntity, bool>> predicate)
        {
            return store.FindWhere(predicate);
        }

        public void Upsert(TEntity entity)
        {
            store.Upsert(entity);
        }
        
    }
}
