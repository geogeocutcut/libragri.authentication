using System;

namespace libragri.core.repository
{
    public interface IUnitOfWork<TId>:IDisposable
    {
        void OpenTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}