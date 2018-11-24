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
using libragri.authentication.specification;

namespace libragri.authentication.service.impl
{
    public class UserService : IUserService
    {
        static LoginSpecification specLogin = new LoginSpecification();
        IFactory factory;
        
        public UserService(IFactory factory)
        {
            this.factory=factory;
        }

        public async Task<UserData> AddUserAsync(UserData user)
        {
            using(var uow = factory.Resolve<IUnitOfWork<string>>())
            {
                if(!specLogin.IsSatisfiedBy(user))
                    throw new ServiceException("Invalid user","The Login must be a valid email !");
                
                user.Email=user.Login;
                var repository = factory.Resolve<IUserRepository>(uow.GetStore());
                if((await repository.FindAsync(u=>u.Login.Equals(user.Login))).Count()>0)
                    throw new ServiceException("Invalid user","This Login already exit !");
                
                return (await repository.UpsertAsync(user));
            }
        }

        public async Task<UserData> AuthentifyAsync(string login, string pwd)
        {
            using(var uow = factory.Resolve<IUnitOfWork<string>>()){
                var repository = factory.Resolve<IUserRepository>(uow.GetStore());
                var user = (await repository.FindAsync(x=>x.Login==login))?.FirstOrDefault();
                if(user==null ||user?.PwdSHA1!=pwd)
                {
                    throw new ServiceException("902","invalid user");
                }
                return user;
            }
        }

        public async Task<UserData> GetByUserNameAsync(string login)
        {
            using(var uow = factory.Resolve<IUnitOfWork<string>>()){
                var repository = factory.Resolve<IUserRepository>(uow.GetStore());
                var user = (await repository.FindAsync(x=>x.Login==login))?.FirstOrDefault();
                return user;
            }
        }
    }
}
