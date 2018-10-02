using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using FindieServer.Interfaces;
using FindieServer.Managers.Interfaces;
using FindieServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace FindieServer.Api.User
{
    [Produces("application/json")]
    [Route("api/User/Location")]
    public class LocationController : Controller, IToken
    {
        private readonly ILocationManager _locationManager;
        public LocationController(ILocationManager locationManager)
        {
            this._locationManager = locationManager;
        }

        [Route("GetSpecificUserLocation")]
        [HttpGet]      
        public async Task<LocationModel> GetSpecificUserLocation(string username)
        {
            return await this._locationManager.GetSpecificUserLocation(username);
        }

        [Route("GetFriendsLocation")]
        [HttpGet]
        public async Task<List<LocationModel>> GetFriendsLocation(string username)
        {
            return await this._locationManager.GetFriendsLocation(username);
        }

        [Route("SendLocation")]
        [HttpPost]
        public async Task SendLocation(LocationModel model)
        {
            if (model != null)
            {
                await this._locationManager.SendLocation(model);
            }
        }
        public string DecodeTokenFromUser()
        {
            var receivedToken = Request.Headers.FirstOrDefault(header => header.Key == "Authorization").Value.ToString().Remove(0, 6).Trim();

            var handler = new JwtSecurityTokenHandler();
            var decodedToken = handler.ReadToken(receivedToken) as JwtSecurityToken;
            return decodedToken.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;
        }
    }
}