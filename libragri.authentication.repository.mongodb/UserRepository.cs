using libragri.authentication.model;
using libragri.authentication.repository.interfaces;
using libragri.core.repository;
using System;

namespace libragri.authentication.repository.mongodb
{
    public class UserRepository : Repository<string, UserModel>, IUserRespository
    {
        public UserRepository(IStore<string, UserModel> storeTmp) : base(storeTmp)
        {
        }

        UserModel IUserRespository.GetByLogin(string Login)
        {
            throw new NotImplementedException();
        }
    }
}
