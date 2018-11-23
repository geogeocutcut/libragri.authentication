using System.Threading.Tasks;
using libragri.authentication.model;
using libragri.authentication.service.interfaces;
using libragri.core.common;
using Microsoft.AspNetCore.Mvc;

namespace libragri.authentication.api.Controllers
{
    [Route("api/v1/user")]
    public class UsersController : Controller
    {
        private IFactory _factory;
        public UsersController(IFactory factory)
        {
            this._factory = factory;
        }

        [Route("add")]
        [HttpPost]
 
        public async Task<IActionResult> CreateUserAsync([FromBody] UserData user)
        {
            var userService = _factory.Resolve<IUserService>(_factory);
            return Ok(await userService.AddUserAsync(user));
        }
    }
}