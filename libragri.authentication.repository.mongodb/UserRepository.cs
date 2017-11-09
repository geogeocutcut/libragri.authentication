using libragri.authentication.model;
using libragri.authentication.repository.interfaces;
using libragri.core.repository;
using System;
using libragri.core.common;

namespace libragri.authentication.repository.mongodb
{
    public class UserRepository<TId> : Repository<TId, UserData<TId>>, IUserRepository<TId>
    {
        public UserRepository(IUnitOfWork<TId> uow) : base(uow)
        {

        }
    }
}
