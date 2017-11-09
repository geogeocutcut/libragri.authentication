using System;
using System.Collections.Generic;
using libragri.authentication.model;

namespace libragri.authentication.service.interfaces
{
    public interface IRefreshTokenService<TId>
    {
        void Add(RefreshTokenData<TId> token);
        void ExpireToken(RefreshTokenData<TId> token);
        RefreshTokenData<TId> CheckRefreshToken(string token,string cliendid);
    }
}
