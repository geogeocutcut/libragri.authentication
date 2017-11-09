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
    public class RefreshTokenService<TId> : IRefreshTokenService<TId>
    {
        IFactory factory;
        
        public RefreshTokenService(IFactory factory)
        {
            this.factory=factory;
        }
        public void Add(RefreshTokenData<TId> token)
        {
            using(var uow = factory.Resolve<IUnitOfWork<TId>>()){
                var repository = factory.Resolve<IRefreshTokenRepository<TId>>(uow);
                repository.Upsert(token);
            }
        }

        public RefreshTokenData<TId> CheckRefreshToken(string token,string cliendid)
        {
            using(var uow = factory.Resolve<IUnitOfWork<TId>>()){
                var repository = factory.Resolve<IRefreshTokenRepository<TId>>(uow);
                var refreshtoken = repository.FindWhere(x=> x.Token==token && x.ClientId==cliendid)?.FirstOrDefault();
                if(refreshtoken==null)
                {
                    throw new ServiceException("905","invalid refresh token");
                }
                return refreshtoken;
            }
        }
        public void ExpireToken(RefreshTokenData<TId> token)
        {
            using(var uow = factory.Resolve<IUnitOfWork<TId>>()){
                var repository = factory.Resolve<IRefreshTokenRepository<TId>>(uow);
                repository.Delete(token);
            }
        }
    }
}
