using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using libragri.authentication.model;

namespace libragri.authentication.service.interfaces
{
    public interface IRefreshTokenService
    {
        Task AddAsync(RefreshTokenData token);
        Task ExpireTokenAsync(RefreshTokenData token);
        Task<RefreshTokenData> CheckRefreshTokenAsync(string token,string cliendid);
    }
}
