using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using libragri.authentication.model;

namespace libragri.authentication.api.Controllers
{
    public class TokensController : Controller
    {
        // GET oauth/token
        [HttpGet]
        [Route("oauth/token")]
        public IEnumerable<string> GetToken(UserModel user)
        {
            return new string[] { "value1", "value2" };
        }


        // GET token/refresh
        [HttpGet]
        [Route("token/refresh")]
        public IEnumerable<string> RefreshToken()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
