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
using System.Threading.Tasks;

namespace libragri.authentication.service.impl
{
    public class UserService<TId> : IUserService<TId>
    {
        IFactory factory;
        
        public UserService(IFactory factory)
        {
            this.factory=factory;
        }

        public async Task<UserData<TId>> AddUserAsync(UserData<TId> user)
        {
            using(var uow = factory.Resolve<IUnitOfWork<TId>>()){
                var repository = factory.Resolve<IUserRepository<TId>>(uow);
                return (await repository.UpsertAsync(user));
            }
        }

        public async Task<UserData<TId>> AuthentifyAsync(string username, string pwd)
        {
            using(var uow = factory.Resolve<IUnitOfWork<TId>>()){
                var repository = factory.Resolve<IUserRepository<TId>>(uow);
                var user = (await repository.FindAsync(x=>x.UserName==username))?.FirstOrDefault();
                if(user==null ||user?.PwdSHA1!=pwd)
                {
                    throw new ServiceException("902","invalid user");
                }
                return user;
            }
        }

        public async Task<UserData<TId>> GetByUserNameAsync(string username)
        {
            using(var uow = factory.Resolve<IUnitOfWork<TId>>()){
                var repository = factory.Resolve<IUserRepository<TId>>(uow);
                var user = (await repository.FindAsync(x=>x.UserName==username))?.FirstOrDefault();
                return user;
            }
        }
    }
}
