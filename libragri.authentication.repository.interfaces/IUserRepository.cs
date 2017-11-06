using libragri.authentication.model;
using libragri.core.repository;
using System;

namespace libragri.authentication.repository.interfaces
{
    public interface IUserRespository:IRepository<string,UserModel>
    {
        UserModel GetByLogin(string Login);
    }
}
