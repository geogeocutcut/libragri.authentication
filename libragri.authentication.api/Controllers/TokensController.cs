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

            var user = userService.Authentify(parameters.username,parameters.password);                                   


            var refresh_token = Guid.NewGuid().ToString().Replace("-", "");

            var token = new RefreshTokenData<string>
            {
                ClientId = parameters.client_id,
                Token = refresh_token,
                Id = Guid.NewGuid().ToString(),
                IsStop = 0,
                UserName = user.UserName
            };

            //store the refresh_token 
            refreshtokenService.Add(token);
            return GenerateJwt(parameters.client_id, user.UserName, refresh_token, _settings.Value.ExpireMinutes);
            
        }

        //scenario 2 ? get the access_token by refresh_token
        private async Task<AuthenticationToken> DoRefreshTokenAsync(Parameters parameters)
        {
            
            var refreshtokenService = _factory.Resolve<IRefreshTokenService<string>>(_factory);

            var token = refreshtokenService.CheckRefreshToken(parameters.refresh_token, parameters.client_id);

            var refresh_token = Guid.NewGuid().ToString().Replace("-", "");

            token.IsStop = 1;

            //expire the old refresh_token and add a new refresh_token
            refreshtokenService.ExpireToken(token);

            refreshtokenService.Add(new RefreshTokenData<string>
            {
                ClientId = parameters.client_id,
                Token = refresh_token,
                Id = Guid.NewGuid().ToString(),
                IsStop = 0,
                UserName = token.UserName
            });

            return GenerateJwt(parameters.client_id, token.UserName, refresh_token, _settings.Value.ExpireMinutes);
        }

        private AuthenticationToken GenerateJwt(string clientId, string userName, string refreshToken, int expireMinutes)

        {

            var now = DateTime.UtcNow;



            var claims = new Claim[]

            {

                new Claim(JwtRegisteredClaimNames.Sub, clientId),

                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

                new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64),

                new Claim("Name", userName)

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
