using System;
using System.Collections.Generic;
using libragri.authentication.repository.interfaces;
using libragri.authentication.service.interfaces;
using libragri.authentication.model;
using libragri.core.common;
using libragri.core.repository;
using System.Text;
using System.Security.Claims;
using System.Linq;

namespace libragri.authentication.service.impl
{
    public class UserService : IUserService
    {
        IFactory factory;
        
        public UserService(IFactory factory)
        {
            this.factory=factory;
        }

        public UserData Authentify(string username, string pwd)
        {
            using(var uow = factory.Resolve<IUnitOfWork>()){
                var repository = factory.Resolve<IUserRepository>(uow);
                var user = repository.FindWhere(x=>x.UserName==username).FirstOrDefault();
                if(user==null ||user?.PwdSHA1!=pwd)
                {
                    throw new ServiceException("902","invalid user");
                }
                return user;
            }
        }

        public UserData GetByUserName(string username)
        {
            using(var uow = factory.Resolve<IUnitOfWork>()){
                var repository = factory.Resolve<IUserRepository>(uow);
                var user = repository.FindWhere(x=>x.UserName==username).FirstOrDefault();
                return user;
            }
        }
    }
}
