using libragri.core.common;
using libragri.core.repository;

namespace libragri.authentication.repository.mongodb
{
    public class UnitOfWork<TId> : IUnitOfWork<TId>
    {
        public IStore<TId> Store { get; set; }
        public UnitOfWork(IStore<TId> Store )
        {
            this.Store=Store;
        }
        public void CommitTransaction()
        {
        }

        public void Dispose()
        {
        }

        public void OpenTransaction()
        {
        }

        public void RollbackTransaction()
        {
        }
    }
}