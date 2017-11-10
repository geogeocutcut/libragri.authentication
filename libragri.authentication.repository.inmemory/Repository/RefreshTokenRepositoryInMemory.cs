﻿using libragri.authentication.model;
using libragri.authentication.repository.interfaces;
using libragri.core.repository;
using System;

namespace libragri.authentication.repository.inmemory
{
    public class RefreshTokenRepositoryInMemory<TId> : RepositoryInMemory<TId, RefreshTokenData<TId>>, IRefreshTokenRepository<TId>
    {
        public RefreshTokenRepositoryInMemory(UnitOfWorkInMemory<TId> uow) : base(uow)
        {

        }
    }
}