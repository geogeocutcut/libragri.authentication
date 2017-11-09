using System;
using System.Collections.Generic;
using libragri.authentication.model;

namespace libragri.authentication.service.interfaces
{
    public interface IUserService<TId>
    {
        UserData<TId> GetByUserName(string username);
        UserData<TId> Authentify(string username,string pwd);
    }
}
