using libragri.authentication.model;
using libragri.authentication.repository.interfaces;
using libragri.core.repository;
using System;
using libragri.core.repository.mongodb;

namespace libragri.authentication.repository.mongodb
{
    public class RefreshTokenRepositoryMongodb : Repository<string, RefreshTokenData>, IRefreshTokenRepository
    {
        public RefreshTokenRepositoryMongodb(IStore<string> store) : base(store)
        {

        }
    }
}
