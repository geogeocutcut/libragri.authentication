using libragri.authentication.model;
using libragri.authentication.repository.interfaces;
using libragri.core.repository;
using System;
using libragri.core.common;

namespace libragri.authentication.repository.mongodb
{
    public class RefreshTokenRepository<TId> : Repository<TId, RefreshTokenData<TId>>, IRefreshTokenRepository<TId>
    {
        public RefreshTokenRepository(IUnitOfWork<TId> uow) : base(uow)
        {

        }
    }
}
