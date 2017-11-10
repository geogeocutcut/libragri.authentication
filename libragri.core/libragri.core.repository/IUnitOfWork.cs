using System;

namespace libragri.core.repository
{
    public interface IUnitOfWork<TId>:IDisposable
    {
        IStore<TId> Store{get;set;}
        void OpenTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}