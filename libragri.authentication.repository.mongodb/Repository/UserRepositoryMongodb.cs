using libragri.authentication.model;
using libragri.authentication.repository.interfaces;
using libragri.core.repository;
using System;
using libragri.core.repository.mongodb;

namespace libragri.authentication.repository.mongodb
{
    public class UserRepositoryMongodb<TId> : RepositoryMongodb<TId, UserData<TId>>, IUserRepository<TId>
    {
        public UserRepositoryMongodb(UnitOfWorkMongodb<TId> uow) : base(uow)
        {

        }
    }
}
