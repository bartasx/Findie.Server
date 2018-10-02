using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FindieServer.Managers.Interfaces;
using FindieServer.Models.DbModels;
using FindieServer.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FindieServer.Managers
{
    public class MessageManager : IMessageManager
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly DatabaseContext _databaseContext;
        public MessageManager(UserManager<AppUser> userManager, DatabaseContext databaseContext)
        {
            this._userManager = userManager;
            this._databaseContext = databaseContext;
        }
        public async Task<List<Messages>> GetMessages(string username, string secondUsername)
        {
            var userId = this._userManager.FindByNameAsync(username).Result.Id;
            var secondUserId = this._userManager.FindByNameAsync(secondUsername).Result.Id;

            var query = await (from p in this._databaseContext.Messages
                where userId == p.MessageFrom && secondUserId == p.MessageTo ||
                      userId == p.MessageTo && secondUserId == p.MessageFrom
                select p).ToListAsync();

            return query;
        }

        public async void SendMessage(string fromUsername, string toUsername, string message)
        {
            var newMessage = new Messages
            {
                MessageFrom = this._databaseContext.Users.FirstOrDefault(x => x.UserName == fromUsername).Id,
                MessageTo = this._databaseContext.Users.FirstOrDefault(x => x.UserName == toUsername).Id,
                MessageText = message,
                TimeStamp = DateTime.Now
            };

            this._databaseContext.Messages.Add(newMessage);

            await this._databaseContext.SaveChangesAsync();
        }

        public Task<Messages> DeleteMessage(Messages message)
        {
            throw new System.NotImplementedException();
        }
    }
}