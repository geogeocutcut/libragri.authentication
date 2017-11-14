using libragri.authentication.model;
using libragri.authentication.repository.interfaces;
using libragri.core.repository;
using System;
using libragri.core.repository.mongodb;

namespace libragri.authentication.repository.mongodb
{
    public class RefreshTokenRepositoryMongodb<TId> : RepositoryMongodb<TId, RefreshTokenData<TId>>, IRefreshTokenRepository<TId>
    {
        public RefreshTokenRepositoryMongodb(UnitOfWorkMongodb<TId> uow) : base(uow)
        {

        }
    }
}
