using libragri.authentication.model;
using libragri.authentication.repository.interfaces;
using libragri.core.repository;
using System;
using libragri.core.repository.inmemorydb;

namespace libragri.authentication.repository.inmemory
{
    public class RefreshTokenRepositoryInMemory : RepositoryInMemory<string, RefreshTokenData>, IRefreshTokenRepository
    {
        public RefreshTokenRepositoryInMemory(UnitOfWorkInMemory<string> uow) : base(uow)
        {

        }
    }
}
