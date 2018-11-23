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
    public class RefreshTokenService : IRefreshTokenService
    {
        IFactory factory;
        
        public RefreshTokenService(IFactory factory)
        {
            this.factory=factory;
        }
        public async Task AddAsync(RefreshTokenData token)
        {
            using(var uow = factory.Resolve<IUnitOfWork<string>>()){
                var repository = factory.Resolve<IRefreshTokenRepository>(uow);
                await repository.UpsertAsync(token);
            }
        }

        public async Task<RefreshTokenData> CheckRefreshTokenAsync(string token,string cliendid)
        {
            using(var uow = factory.Resolve<IUnitOfWork<string>>()){
                var repository = factory.Resolve<IRefreshTokenRepository>(uow);
                var refreshtoken = (await repository.FindAsync(x=> x.Token==token && x.ClientId==cliendid))?.FirstOrDefault();
                if(refreshtoken==null)
                {
                    throw new ServiceException("905","invalid refresh token");
                }
                return refreshtoken;
            }
        }
        public async Task ExpireTokenAsync(RefreshTokenData token)
        {
            using(var uow = factory.Resolve<IUnitOfWork<string>>()){
                var repository = factory.Resolve<IRefreshTokenRepository>(uow);
                await repository.DeleteAsync(token);
            }
        }
    }
}
