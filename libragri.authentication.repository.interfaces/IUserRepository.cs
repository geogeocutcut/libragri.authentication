using libragri.authentication.model;
using libragri.core.repository;
using System;

namespace libragri.authentication.repository.interfaces
{
    public interface IUserRepository:IRepository<string,UserData>
    {
    }
}
