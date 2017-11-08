using System;
using System.Collections.Generic;
using libragri.authentication.model;

namespace libragri.authentication.service.interfaces
{
    public interface IUserService
    {
        UserData GetByUserName(string username);
        UserData Authentify(string username,string pwd);
    }
}
