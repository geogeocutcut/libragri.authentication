using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using libragri.authentication.model;

namespace libragri.authentication.service.interfaces
{
    public interface IUserService
    {
        Task<UserData> AddUserAsync(UserData user);
        Task<UserData> GetByUserNameAsync(string username);
        Task<UserData> AuthentifyAsync(string username,string pwd);
    }
}
