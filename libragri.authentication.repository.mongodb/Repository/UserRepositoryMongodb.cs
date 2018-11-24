using libragri.authentication.model;
using libragri.authentication.repository.interfaces;
using libragri.core.repository;
using System;
using libragri.core.repository.mongodb;

namespace libragri.authentication.repository.mongodb
{
    public class UserRepositoryMongodb : Repository<string, UserData>, IUserRepository
    {
        public UserRepositoryMongodb(IStore<string> store) : base(store)
        {

        }
    }
}
