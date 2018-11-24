using libragri.authentication.model;
using libragri.authentication.repository.interfaces;
using libragri.core.repository;
using System;

namespace libragri.authentication.repository.inmemory
{
    public class RefreshTokenRepositoryInMemory : Repository<string, RefreshTokenData>, IRefreshTokenRepository
    {
        public RefreshTokenRepositoryInMemory(IStore<string> store) : base(store)
        {

        }
    }
}
