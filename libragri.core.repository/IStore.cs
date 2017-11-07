
using libragri.core.cqrs;
using System;
using System.Collections.Generic;
using System.Text;

namespace libragri.core.repository
{
    public interface IStore<TId, TEntity> where TEntity : AggregateRoot<TId>
    {
        TEntity FindById(TId id);
        void Upsert(TEntity entity);
        void Remove(TEntity entity);
        IList<TEntity> FindAll();

        void OpenTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
