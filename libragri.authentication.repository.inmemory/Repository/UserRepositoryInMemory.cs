using libragri.authentication.model;
using libragri.authentication.repository.interfaces;
using libragri.core.repository;
using System;
using libragri.core.repository.inmemorydb;

namespace libragri.authentication.repository.inmemory
{
    public class UserRepositoryInMemory : Repository<string, UserData>, IUserRepository
    {
        public UserRepositoryInMemory(IStore<string> store) : base(store)
        {

        }
    }
}
