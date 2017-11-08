using libragri.authentication.model;
using libragri.authentication.repository.interfaces;
using libragri.core.repository;
using System;
using libragri.core.common;

namespace libragri.authentication.repository.mongodb
{
    public class RefreshTokenRepository : Repository<string, RefreshTokenData>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(IStore<string, RefreshTokenData> store) : base(store)
        {

        }
    }
}
