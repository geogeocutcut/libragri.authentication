using System;

namespace libragri.core.repository
{
    public interface IUnitOfWork:IDisposable
    {
        void OpenTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}