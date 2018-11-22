using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using libragri.authentication.model;
using libragri.core.common;
using libragri.authentication.service.interfaces;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;

namespace libragri.authentication.api.Controllers
{
    public class TokensController : Controller
    {
        private IOptions<Audience> _settings;
        private IFactory _factory;
        public TokensController(IOptions<Audience> settings,IFactory factory)
        {
            this._factory = factory;
            this._settings = settings;
        }


        [Route("oauth/token")]
        public async Task<IActionResult> AuthAsync(Parameters parameters)
        {
            if (parameters == null)
            {
                throw new ServiceException("bad request","paramters are missing.");
            }

            if (parameters.grant_type == "password")
            {
                return Ok(await DoPasswordAsync(parameters));
            }
            else if (parameters.grant_type == "refresh_token")
            {
                return Ok(await DoRefreshTokenAsync(parameters));
            }
            else
            {
                throw new ServiceException("bad request","failed authentication.");
            }
        }

        [Route("oauth/validatetoken")]
        public async Task<IActionResult> ValidateTokenAsync(AuthenticationToken jwt)
        {
            var symmetricKeyAsBase64 = _settings.Value.Secret;
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);

            TokenValidationParameters validationParameters =
            new TokenValidationParameters
            {
                ValidIssuer = _settings.Value.Iss,
                ValidAudience = _settings.Value.Aud,
                IssuerSigningKey = signingKey
            };

            SecurityToken validatedToken;
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var user = handler.ValidateToken(jwt.access_token, validationParameters, out validatedToken);
            return Ok(new {message="Token is valid"});
        }
        private async Task<AuthenticationToken> DoPasswordAsync(Parameters parameters)
        {
            //validate the client_id/client_secret/username/password       
            var userService = _factory.Resolve<IUserService<string>>(_factory);
            var refreshtokenService = _factory.Resolve<IRefreshTokenService<string>>(_factory);

            var user = await userService.AuthentifyAsync(parameters.username,parameters.password);                                   


            var refresh_token = Guid.NewGuid().ToString().Replace("-", "");

            var token = new RefreshTokenData<string>
            {
                ClientId = parameters.client_id,
                Token = refresh_token,
                Id = Guid.NewGuid().ToString(),
                IsStop = 0,
                UserName = user.UserName,
                UserId = user.Id
            };

            //store the refresh_token 
            await refreshtokenService.AddAsync(token);
            return GenerateJwt(parameters.client_id,user.Id, user.UserName, refresh_token, _settings.Value.ExpireMinutes);
            
        }

        //scenario 2 ? get the access_token by refresh_token
        private async Task<AuthenticationToken> DoRefreshTokenAsync(Parameters parameters)
        {
            
            var refreshtokenService = _factory.Resolve<IRefreshTokenService<string>>(_factory);

            var token = await refreshtokenService.CheckRefreshTokenAsync(parameters.refresh_token, parameters.client_id);

            var refresh_token = Guid.NewGuid().ToString().Replace("-", "");

            token.IsStop = 1;

            //expire the old refresh_token and add a new refresh_token
            await refreshtokenService.ExpireTokenAsync(token);

            await refreshtokenService.AddAsync(new RefreshTokenData<string>
            {
                ClientId = parameters.client_id,
                Token = refresh_token,
                Id = Guid.NewGuid().ToString(),
                IsStop = 0,
                UserName = token.UserName,
                UserId = token.UserId
            });

            return GenerateJwt(parameters.client_id,token.UserId, token.UserName, refresh_token, _settings.Value.ExpireMinutes);
        }

        private AuthenticationToken GenerateJwt(string clientId, string userId, string userName, string refreshToken, int expireMinutes)

        {

            var now = DateTime.UtcNow;



            var claims = new Claim[]

            {

                new Claim(JwtRegisteredClaimNames.Sub, clientId),

                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

                new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64),

                new Claim("UserName", userName),

                new Claim("UserId",userId)

            };



            var symmetricKeyAsBase64 = _settings.Value.Secret;

            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);

            var signingKey = new SymmetricSecurityKey(keyByteArray);



            var jwt = new JwtSecurityToken(

                issuer: _settings.Value.Iss,

                audience: _settings.Value.Aud,

                claims: claims,

                notBefore: now,

                expires: now.Add(TimeSpan.FromMinutes(expireMinutes)),

                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);



            var response = new AuthenticationToken
            {

                access_token = encodedJwt,

                expires_in = (int)TimeSpan.FromMinutes(expireMinutes).TotalSeconds,

                refresh_token = refreshToken,

            };
            
            return response;
        }
    }
}
