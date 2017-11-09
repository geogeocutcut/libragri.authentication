using libragri.authentication.model;
using libragri.authentication.repository.interfaces;
using libragri.core.repository;
using System;

namespace libragri.authentication.repository.inmemory
{
    public class UserRepositoryInMemory<TId> : RepositoryInMemory<TId, UserData<TId>>, IUserRepository<TId>
    {
        public UserRepositoryInMemory(UnitOfWorkInMemory<TId> uow) : base(uow)
        {

        }
    }
}
