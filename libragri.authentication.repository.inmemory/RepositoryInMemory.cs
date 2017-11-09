

using System;
using System.Collections.Generic;
using libragri.core.common;
using libragri.core.repository;

namespace libragri.authentication.repository.inmemory
{
    public class RepositoryInMemory<TId, TEntity> : IRepository<TId, TEntity> where TEntity : Entity<TId>
    {
        IStore<TId> store;

        public RepositoryInMemory(UnitOfWorkInMemory<TId> uow)
        {
            this.store=uow.Store;
        }

        public void Delete(TEntity entity)
        {
            store.Remove<TEntity>(entity);
        }

        public IList<TEntity> GetAll()
        {
            return store.FindAll<TEntity>();
        }

        public TEntity GetById(TId id)
        {
            return store.FindById<TEntity>(id);
        }

        
        public IList<TEntity> FindWhere(System.Linq.Expressions.Expression<System.Func<TEntity, bool>> predicate)
        {
            return store.FindWhere<TEntity>(predicate);
        }

        public void Upsert(TEntity entity)
        {
            store.Upsert<TEntity>(entity);
        }
        
    }
}
