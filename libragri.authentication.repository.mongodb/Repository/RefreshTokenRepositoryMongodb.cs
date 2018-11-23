using libragri.authentication.model;
using libragri.authentication.repository.interfaces;
using libragri.core.repository;
using System;
using libragri.core.repository.mongodb;

namespace libragri.authentication.repository.mongodb
{
    public class RefreshTokenRepositoryMongodb : RepositoryMongodb<string, RefreshTokenData>, IRefreshTokenRepository
    {
        public RefreshTokenRepositoryMongodb(UnitOfWorkMongodb<string> uow) : base(uow)
        {

        }
    }
}
