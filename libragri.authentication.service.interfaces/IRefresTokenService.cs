using System;
using System.Collections.Generic;
using libragri.authentication.model;

namespace libragri.authentication.service.interfaces
{
    public interface IRefreshTokenService
    {
        void Add(RefreshTokenData token);
        void ExpireToken(RefreshTokenData token);
        RefreshTokenData GetByToken(string token,string cliendid);
    }
}
