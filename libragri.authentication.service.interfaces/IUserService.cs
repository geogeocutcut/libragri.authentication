using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using libragri.authentication.model;

namespace libragri.authentication.service.interfaces
{
    public interface IUserService<TId>
    {
        Task<UserData<TId>> AddUserAsync(UserData<TId> username);
        Task<UserData<TId>> GetByUserNameAsync(string username);
        Task<UserData<TId>> AuthentifyAsync(string username,string pwd);
    }
}
