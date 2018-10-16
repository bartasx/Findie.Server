using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Findie.Common.Models;
using Findie.Common.Models.IdentityModels;
using FindieServer.DbModels;
using FindieServer.Managers.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FindieServer.Managers
{
    public class LocationManager : ILocationManager
    {
        private readonly DatabaseContext _context;
        private readonly UserManager<AppUser> _userManager;

        public LocationManager(DatabaseContext databseContext, UserManager<AppUser> userManager)
        {
            this._context = databseContext;
            this._userManager = userManager;
        }

        public async Task<List<LocationModel>> GetFriendsLocation(string username)
        {
            var user = this._userManager.FindByNameAsync(username).Result.Id;

            var query =
                await (from p in this._context.Friends
                    where p.FirstUser == user || p.SecondUser == user
                    select p).Distinct().ToListAsync();

            var friendsIdList = new List<int?>();

            foreach (var friend in query)
            {
                if (friend.FirstUser == user)
                {
                    friendsIdList.Add(friend.SecondUser);
                }
                else if (friend.SecondUser == user)
                {
                    friendsIdList.Add(friend.FirstUser);
                }
            }

            var friendsList =
                await (from p in this._context.Users where friendsIdList.Contains(p.Id) select p).ToListAsync();

            var friendsLocationList = new List<LocationModel>();

            foreach (var friend in friendsList)
            {
                friendsLocationList.Add(new LocationModel()
                {
                    Latitude = (double) friend.Latitude,
                    Longitude = (double) friend.Longitude,
                    Username = friend.UserName
                });
            }

            return friendsLocationList;
        }

        public async Task<LocationModel> GetSpecificUserLocation(string username)
        {
            var query =
                await (from p in this._context.Users where username == p.UserName select p).FirstOrDefaultAsync();

            if (query != null)
            {
                var model = new LocationModel()
                {
                    Username = username,
                    Latitude = (double) query.Latitude,
                    Longitude = (double) query.Longitude
                };
                return model;
            }

            return null;
        }

        public async Task SendLocation(LocationModel model)
        {
            var normalizedUsername = model.Username.ToUpper();
            var query = await (from users in this._context.Users
                where users.NormalizedUserName == normalizedUsername
                select users).FirstOrDefaultAsync();

            if (query != null)
            {
                query.Latitude = (decimal) model.Latitude;
                query.Longitude = (decimal) model.Longitude;

                await this._context.SaveChangesAsync();
            }
        }
    }
}