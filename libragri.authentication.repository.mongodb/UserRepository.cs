using libragri.authentication.model;
using libragri.authentication.repository.interfaces;
using libragri.core.repository;
using System;
using libragri.core.common;

namespace libragri.authentication.repository.mongodb
{
    public class UserRepository : Repository<string, UserData>, IUserRepository
    {
        public UserRepository(IStore<string, UserData> store) : base(store)
        {

        }
    }
}
