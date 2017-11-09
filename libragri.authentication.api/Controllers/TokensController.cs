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
        // GET oauth/token
        [HttpGet]
        [Route("oauth/token")]
        public IActionResult GetToken(string login,string pwdSHA1)
        {
            var service = _factory.Resolve<IUserService<string>>(_factory);
            var data = service.Authentify(login,pwdSHA1);
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("test"));

            var claims = new List<Claim>();

            var token = new JwtSecurityToken(
                issuer: "your app",
                audience: "the client of your app",
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddDays(28),
                signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256)
            );
            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return new ObjectResult(jwtToken);
        }

        // GET token/refresh
        [HttpGet]
        [Route("token/refresh")]
        public IEnumerable<string> RefreshToken()
        {
            return new string[] { "value1", "value2" };
        }


        [HttpGet("auth")]
        public async Task<IActionResult> AuthAsync([FromQuery]Parameters parameters)
        {
            if (parameters == null)
            {
                return Json(new ResponseData
                {
                    Code = "901",
                    Message = "Null parameters",
                    Data = null
                });
            }

            if (parameters.grant_type == "password")
            {
                return Json(await DoPasswordAsync(parameters));
            }
            else if (parameters.grant_type == "refresh_token")
            {
                return Json(await DoRefreshTokenAsync(parameters));
            }
            else
            {
                return Json(new ResponseData
                {
                    Code = "904",
                    Message = "Bad request",
                    Data = null
                });
            }
        }

        private async Task<ResponseData> DoPasswordAsync(Parameters parameters)
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
            return new ResponseData
            {
                Code = "999",
                Message = "Ok",
                Data = GenerateJwt(parameters.client_id, user.UserName, refresh_token, _settings.Value.ExpireMinutes)
            };
        }

        //scenario 2 ： get the access_token by refresh_token
        private async Task<ResponseData> DoRefreshTokenAsync(Parameters parameters)
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

            return new ResponseData
            {
                Code = "999",
                Message = "Ok",
                Data = GenerateJwt(parameters.client_id, token.UserName, refresh_token, _settings.Value.ExpireMinutes)
            };
        }

        private string GenerateJwt(string clientId, string userName, string refreshToken, int expireMinutes)

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



            var response = new

            {

                access_token = encodedJwt,

                expires_in = (int)TimeSpan.FromMinutes(expireMinutes).TotalSeconds,

                refresh_token = refreshToken,

            };



            return JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented });

        }
    }
}
