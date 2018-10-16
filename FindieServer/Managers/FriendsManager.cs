using System;
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
    public class FriendsManager : IFriendsManager
    {
        private readonly DatabaseContext _context;
        private readonly UserManager<AppUser> _userManager;

        public FriendsManager(DatabaseContext databaseContext, UserManager<AppUser> userManager)
        {
            this._context = databaseContext;
            this._userManager = userManager;
        }
        public async Task<List<string>> GetFriendsList(string username)
        {
            var userId = this._userManager.FindByNameAsync(username).Id;

            var query = await (from p in this._context.Friends where p.FirstUser == userId || p.SecondUser == userId && p.IsRequestAccepted select p).Distinct().ToListAsync();
            query.RemoveAll(x => x.FirstUser == userId && x.SecondUser == userId);

            var friendsList = new List<string>();

            foreach (var record in query)
            {
                friendsList.Add(this._userManager.FindByIdAsync(record.FirstUser.ToString()).Result.UserName);
                friendsList.Add(this._userManager.FindByIdAsync(record.SecondUser.ToString()).Result.UserName);
            }
            return friendsList;
        }

        public async Task<UserInfo> GetSpecificUserInfo(string username)
        {
            var foundUser = await this._userManager.FindByNameAsync(username);

            if (foundUser != null)
            {
                var userInfoModel = new UserInfo()
                {
                    Username = foundUser.UserName,
                    Email = foundUser.Email,
                    AccountDescription = foundUser.AccountDescription,
                    IsInFriendsList = await this.IsInFriends(),
                    Latitude = (double)foundUser.Latitude,
                    Longitude = (double)foundUser.Longitude,
                    Name = foundUser.Name,
                    Surname = foundUser.Surname,
                };
                return userInfoModel;
            }
            return null;
        }

        public async Task<List<string>> GetUserByNameFromSearchbar(string username)
        {
            var query = await (from users in this._context.Users where users.UserName.StartsWith(username) select users.UserName).ToListAsync();
            if (query != null)
            {
                return query;
            }
            return null;
        }

        public async Task<bool> RemoveFriendFromFriendsList(string senderUsername, string friendUsername)
        {
            var senderId = this._userManager.FindByNameAsync(senderUsername).Id;
            var friendId = this._userManager.FindByNameAsync(friendUsername).Id;

            var query = await (from friendship in this._context.Friends
                               where senderId == friendship.FirstUser && friendId == friendship.SecondUser ||
senderId == friendship.SecondUser && friendId == friendship.FirstUser
                               select friendship).FirstOrDefaultAsync();
            if (query != null)
            {
                this._context.Remove(query);
                await this._context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> SendFriendRequest(string senderUsername, string friendUsername)
        {
            try
            {
                var senderId = this._userManager.FindByNameAsync(senderUsername).Result.Id;
                var friendId = this._userManager.FindByNameAsync(friendUsername).Result.Id;

                var newFriendship = new Friends()
                {
                    FirstUser = senderId,
                    SecondUser = friendId,
                    IsRequestAccepted = false
                };
                await this._context.Friends.AddAsync(newFriendship);
                await this._context.SaveChangesAsync();
                return true;
            }

            catch (Exception ex)
            {
                return false;
            }

        }
        public async Task<bool> AcceptFriendRequest(string username, string friendUsername)
        {
            var user = this._userManager.FindByNameAsync(username).Result.Id;
            var friend = this._userManager.FindByNameAsync(friendUsername).Result.Id;

            var query = await (from p in this._context.Friends
                               where !p.IsRequestAccepted && p.FirstUser == friend && p.SecondUser == user
                               select p).FirstOrDefaultAsync();

            if (query != null)
            {
                query.IsRequestAccepted = true;
                await this._context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        private async Task<bool> IsInFriends()
        {
            return false;
        }
    }
}