using System.Threading.Tasks;
using Findie.Common.Models;
using FindieServer.Managers.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FindieServer.Api.User
{
    [Produces("application/json")]
    [Route("Api/User/Account")]
    public class AccountController : Controller
    {
        private readonly IAccountsManager _accountsManager;
        public AccountController(IAccountsManager accountsManager)
        {
            this._accountsManager = accountsManager;
        }

        [HttpGet]
        [Route("Login")]
        public async Task<IActionResult> LoginUser(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await this._accountsManager.LoginUser(model);
                if (result != null)
                {
                    return Ok(result);
                }
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterNewAccount([FromBody]RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await this._accountsManager.RegisterAccount(model);
                if (result != null)
                {
                    return Ok(result);
                }
            }
            ModelState.AddModelError(string.Empty, "Niepoprawne dane!");
            return Unauthorized();
        }

        [HttpPut]
        [Route("UpdateUserInfo")]
        public async Task<IActionResult> UpdateUserInfo(UserInfo model)
        {
            if (await this._accountsManager.ChangeCredentials(model))
            {
                return Ok();
            }
            return BadRequest();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("Logout")]
        public IActionResult Logout()
        {
            this._accountsManager.Logout();
            return Ok();
        }
    }
}