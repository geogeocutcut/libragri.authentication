using libragri.authentication.model;
using libragri.authentication.repository.interfaces;
using libragri.core.repository;
using System;
using libragri.core.repository.inmemorydb;

namespace libragri.authentication.repository.inmemory
{
    public class UserRepositoryInMemory : RepositoryInMemory<string, UserData>, IUserRepository
    {
        public UserRepositoryInMemory(UnitOfWorkInMemory<string> uow) : base(uow)
        {

        }
    }
}
