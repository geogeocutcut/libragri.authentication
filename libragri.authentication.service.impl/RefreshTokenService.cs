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
    public class RefreshTokenService : IRefreshTokenService
    {
        IFactory factory;
        
        public RefreshTokenService(IFactory factory)
        {
            this.factory=factory;
        }
        public void Add(RefreshTokenData token)
        {
            using(var uow = factory.Resolve<IUnitOfWork>()){
                var repository = factory.Resolve<IRefreshTokenRepository>(uow);
                repository.Upsert(token);
            }
        }

        public RefreshTokenData GetByToken(string token,string cliendid)
        {
            using(var uow = factory.Resolve<IUnitOfWork>()){
                var repository = factory.Resolve<IRefreshTokenRepository>(uow);
                return repository.FindWhere(x=> x.Token==token && x.ClientId==cliendid).FirstOrDefault();
            }
        }
        public void ExpireToken(RefreshTokenData token)
        {
            using(var uow = factory.Resolve<IUnitOfWork>()){
                var repository = factory.Resolve<IRefreshTokenRepository>(uow);
                repository.Delete(token);
            }
        }
    }
}
