using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using libragri.authentication.model;

namespace libragri.authentication.service.interfaces
{
    public interface IRefreshTokenService<TId>
    {
        Task AddAsync(RefreshTokenData<TId> token);
        Task ExpireTokenAsync(RefreshTokenData<TId> token);
        Task<RefreshTokenData<TId>> CheckRefreshTokenAsync(string token,string cliendid);
    }
}
