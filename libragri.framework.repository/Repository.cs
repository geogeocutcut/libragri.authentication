
using libragri.core.cqrs;
using System;
using System.Collections.Generic;

namespace libragri.core.repository
{
    public class Repository<TId, TEntity> : IRepository<TId, TEntity> where TEntity : AggregateRoot<TId>
    {
        IStore<TId, TEntity> store;

        public Repository(IStore<TId, TEntity> storeTmp)
        {
            store=storeTmp;
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

        public void Upsert(TEntity entity)
        {
            store.Upsert(entity);
        }
        
    }
}
