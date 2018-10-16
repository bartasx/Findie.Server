using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Findie.Common.Models;
using Findie.Common.Models.IdentityModels;
using FindieServer.Interfaces;
using FindieServer.Managers.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FindieServer.Api.User
{
    [Produces("application/json")]
    [Route("api/User/Friends")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FriendsController : Controller, IToken
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IFriendsManager _friendsManager;

        public FriendsController(UserManager<AppUser> userManager, IFriendsManager friendsManager)
        {
            this._userManager = userManager;
            this._friendsManager = friendsManager;
        }

        [HttpGet]
        [Route("GetFriendsList")]
        public async Task<List<string>> GetFriendsList(string username)
        {
            return await this._friendsManager.GetFriendsList(username);
        }

        [HttpGet]
        [Route("GetSpecificUserInfo")]
        public async Task<UserInfo> GetSpecificUserInfo(string username)
        {
            return await this._friendsManager.GetSpecificUserInfo(username);
        }

        [HttpGet]
        [Route("SearchUserByUsername")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<List<string>> SearchUserByName(string username)
        {
            return await this._friendsManager.GetUserByNameFromSearchbar(username);
        }

        [HttpPost]
        [Route("SendFriendRequest")]
        public async Task<IActionResult> SendFriendRequest(string username)
        {
            if (await this._friendsManager.SendFriendRequest(this.DecodeTokenFromUser(), username))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("AcceptFriendRequest")]
        public async Task<IActionResult> AcceptFriendRequest(string username)
        {
            if (await this._friendsManager.AcceptFriendRequest(this.DecodeTokenFromUser(), username))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("RemoveFriend")]
        public async Task<IActionResult> RemoveFriend(string username)
        {
            if (await this._friendsManager.RemoveFriendFromFriendsList(this.DecodeTokenFromUser(), username))
            {
                return Ok();
            }
            return BadRequest();
        }

        public string DecodeTokenFromUser()
        {
            var receivedToken = Request.Headers.FirstOrDefault(header => header.Key == "Authorization").Value.ToString().Remove(0,6).Trim();

            var handler = new JwtSecurityTokenHandler();
            var decodedToken = handler.ReadToken(receivedToken) as JwtSecurityToken;
            return decodedToken.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;
        }
    }
}